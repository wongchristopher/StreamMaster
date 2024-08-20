﻿using StreamMaster.Domain.Events;
using StreamMaster.Streams.Domain.Events;

using System.Collections.Concurrent;

namespace StreamMaster.Streams.Handlers
{
    /// <summary>
    /// Service for managing the status of channels, including creation, monitoring, and disposal.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ChannelStatusService"/> class.
    /// </remarks>
    /// <param name="logger">Logger for this service.</param>
    /// <param name="channelStatusLogger">Logger for channel status.</param>
    public class ChannelBroadcasterService(ILogger<ChannelBroadcasterService> logger, IOptionsMonitor<Setting> _settings, ICacheManager cacheManager, ILogger<IChannelBroadcaster> channelStatusLogger)
        : IChannelBroadcasterService
    {
        /// <inheritdoc/>
        public event AsyncEventHandler<ChannelBroascasterStopped>? _OnChannelBroadcasterStoppedEvent;

        private readonly ConcurrentDictionary<int, IChannelBroadcaster> _sourceChannelBroadcasters = new();
        private readonly SemaphoreSlim _getOrCreateSourceChannelBroadcasterSlim = new(1, 1);

        /// <inheritdoc/>
        public IDictionary<int, IStreamHandlerMetrics> GetMetrics()
        {
            Dictionary<int, IStreamHandlerMetrics> metrics = [];

            foreach (KeyValuePair<int, IChannelBroadcaster> kvp in _sourceChannelBroadcasters)
            {
                IChannelBroadcaster channelBroadcaster = kvp.Value;
                metrics[kvp.Key] = channelBroadcaster.Metrics;
            }

            return metrics;
        }

        /// <inheritdoc/>
        public async Task<IChannelBroadcaster> GetOrCreateChannelBroadcasterAsync(IClientConfiguration config, int streamGroupProfileId, CancellationToken cancellationToken)
        {
            await _getOrCreateSourceChannelBroadcasterSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (_sourceChannelBroadcasters.TryGetValue(config.SMChannel.Id, out IChannelBroadcaster? channelBroadcaster))
                {
                    if (channelBroadcaster.IsFailed)
                    {
                        await UnRegisterChannelAsync(config.SMChannel.Id);
                    }
                    else
                    {
                        logger.LogInformation("Reusing channel broadcaster: {Id} {Name}", config.SMChannel.Id, config.SMChannel.Name);
                        return channelBroadcaster;
                    }
                }

                channelBroadcaster = new ChannelBroadcaster(channelStatusLogger, config.SMChannel)
                {
                    SMChannel = config.SMChannel,
                    StreamGroupProfileId = streamGroupProfileId
                };

                logger.LogInformation("Created new channel for: {Id} {Name}", config.SMChannel.Id, config.SMChannel.Name);

                channelBroadcaster.OnChannelBroadcasterStoppedEvent += async (sender, args) => await OnStoppedEvent(sender, args).ConfigureAwait(false);
                _sourceChannelBroadcasters.TryAdd(config.SMChannel.Id, channelBroadcaster);

                return channelBroadcaster;
            }
            finally
            {
                _getOrCreateSourceChannelBroadcasterSlim.Release();
            }
        }

        /// <inheritdoc/>
        public List<IChannelBroadcaster> GetChannelBroadcasters()
        {
            return [.. _sourceChannelBroadcasters.Values];
        }

        private async Task CheckForEmptyBroadcastersAsync(CancellationToken cancellationToken = default)
        {

            foreach (IChannelBroadcaster? channelBroadcaster in cacheManager.ChannelBroadcasters.Values.Where(a => a.SMStreamInfo != null))
            {
                if (channelBroadcaster.ClientChannelWriters.IsEmpty)
                {
                    await StopChannelAsync(channelBroadcaster).ConfigureAwait(false);
                }
            }

        }


        ///// <inheritdoc/>
        //public bool StopBroadcaster(int key)
        //{
        //    if (_sourceChannelBroadcasters.TryRemove(key, out IChannelBroadcaster? channelBroadcaster))
        //    {
        //        channelBroadcaster.Stop();
        //        return true;
        //    }

        //    return false;
        //}

        public async Task StopChannelAsync(IChannelBroadcaster channelBroadcaster)
        {
            await StopChannelAsync(channelBroadcaster.Id).ConfigureAwait(false);
        }

        public async Task StopChannelAsync(int channelBroadcasterId)
        {
            if (!_sourceChannelBroadcasters.TryGetValue(channelBroadcasterId, out IChannelBroadcaster? sourceChannelBroadcaster))
            {
                return;
            }

            foreach (IClientConfiguration config in sourceChannelBroadcaster.GetClientStreamerConfigurations())
            {
                await UnRegisterClientAsync(config.UniqueRequestId).ConfigureAwait(false);
            }


            sourceChannelBroadcaster.Shutdown = true;
            int delay = _settings.CurrentValue.ShutDownDelay;
            if (delay > 0)
            {
                await UnRegisterChannelAfterDelayAsync(sourceChannelBroadcaster, TimeSpan.FromMilliseconds(delay), CancellationToken.None).ConfigureAwait(false);
            }
            else
            {
                await UnRegisterChannelAsync(sourceChannelBroadcaster.SMChannel.Id).ConfigureAwait(false);
            }
        }

        private async Task<bool> UnRegisterChannelAfterDelayAsync(IChannelBroadcaster channelBroadcaster, TimeSpan delay, CancellationToken cancellationToken)
        {
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);

            return channelBroadcaster.ClientCount == 0 && channelBroadcaster.SMStreamInfo != null
                && await UnRegisterChannelAsync(channelBroadcaster.SMChannel.Id).ConfigureAwait(false);
        }

        private async Task<bool> UnRegisterChannelAsync(int channelBroadcasterId)
        {
            _sourceChannelBroadcasters.TryRemove(channelBroadcasterId, out _);

            if (cacheManager.ChannelBroadcasters.TryRemove(channelBroadcasterId, out IChannelBroadcaster? channelBroadcaster))
            {

                foreach (IClientConfiguration config in channelBroadcaster.GetClientStreamerConfigurations())
                {
                    await UnRegisterClientAsync(config.UniqueRequestId).ConfigureAwait(false);
                    config.Stop();
                }

                channelBroadcaster.Stop();
                return true;
            }

            return false;
        }


        //IChannelBroadcaster channelBroadcaster in cacheManager.ChannelBroadcasters.Values causes to not flsuh clients
        public async Task<bool> UnRegisterClientAsync(string uniqueRequestId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            bool removed = false;
            foreach (IChannelBroadcaster channelBroadcaster in cacheManager.ChannelBroadcasters.Values)
            {
                //IClientConfiguration? clientConfig = channelBroadcaster.GetClientStreamerConfigurations().Find(config => config.UniqueRequestId == uniqueRequestId);

                //if (clientConfig != null)
                //{
                //    if (channelBroadcaster.RemoveClientStreamer(uniqueRequestId))
                //    {

                //        logger.LogDebug("Client configuration for {UniqueRequestId} removed", uniqueRequestId);
                //        removed = true;
                //    }
                //}

                if (channelBroadcaster.RemoveClientStreamer(uniqueRequestId))
                {

                    logger.LogDebug("Client configuration for {UniqueRequestId} removed", uniqueRequestId);
                    removed = true;
                }

            }



            if (removed)
            {
                await CheckForEmptyBroadcastersAsync(cancellationToken).ConfigureAwait(false);
                return true;
            }

            logger.LogDebug("Client configuration for {UniqueRequestId} not found", uniqueRequestId);
            return false;
        }

        /// <inheritdoc/>
        private async Task OnStoppedEvent(object? sender, ChannelBroascasterStopped e)
        {
            if (sender is IChannelBroadcaster channelBroadcaster)
            {
                channelBroadcaster.Shutdown = true;

                await StopChannelAsync(e.Id);
                _OnChannelBroadcasterStoppedEvent?.Invoke(sender, e);
            }
        }
    }
}
