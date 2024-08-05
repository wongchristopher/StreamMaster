﻿using StreamMaster.Domain.Events;

using System.Collections.Concurrent;

namespace StreamMaster.Streams.Services
{
    public class ChannelStatusService : IChannelStatusService
    {
        public event AsyncEventHandler<ChannelStatusStopped>? OnChannelStatusStoppedEvent;

        private readonly ConcurrentDictionary<int, IChannelStatus> _sourceChannelDistributors = new();
        private readonly SemaphoreSlim GetOrCreateSourceChannelDistributorSlim = new(1, 1);
        private readonly ILogger<ChannelStatusService> _logger;
        private readonly ILogger<IChannelBroadcaster> _channelStatusLogger;
        private readonly IVideoInfoService _videoInfoService;

        public ChannelStatusService(
            ILogger<ChannelStatusService> logger,
            ILogger<IChannelBroadcaster> channelStatusLogger,
            IVideoInfoService videoInfoService)
        {
            _logger = logger;
            _channelStatusLogger = channelStatusLogger;
            _videoInfoService = videoInfoService;
        }

        public async Task<IChannelStatus> GetOrCreateChannelStatusAsync(IClientConfiguration config, CancellationToken cancellationToken)
        {
            await GetOrCreateSourceChannelDistributorSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (_sourceChannelDistributors.TryGetValue(config.SMChannel.Id, out IChannelStatus? channelStatus))
                {
                    if (channelStatus.IsFailed)
                    {
                        _ = StopAndUnRegisterChannelStatus(config.SMChannel.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Reusing channel distributor: {Id} {name}", config.SMChannel.Id, config.SMChannel.Name);
                        return channelStatus;
                    }
                }
                ChannelStatus a = new(_channelStatusLogger, config.SMChannel)
                {
                    SMChannel = config.SMChannel,
                    StreamGroupId = config.StreamGroupId,
                    StreamGroupProfileId = config.StreamGroupProfileId
                };

                channelStatus = a;

                _logger.LogInformation("Created new channel status: {Id} {name}", config.SMChannel.Id, config.SMChannel.Name);

                channelStatus.OnChannelStatusStoppedEvent += OnChannelStatusStopped;
                bool test = _sourceChannelDistributors.TryAdd(config.SMChannel.Id, channelStatus);

                return channelStatus;
            }
            finally
            {
                GetOrCreateSourceChannelDistributorSlim.Release();
            }
        }

        public bool StopAndUnRegisterChannelStatus(int key)
        {
            if (_sourceChannelDistributors.TryRemove(key, out IChannelStatus? channelStatus))
            {
                channelStatus.Stop();
                return true;
            }

            return false;
        }

        public IDictionary<int, IChannelStatus> GetChannelStatuses()
        {
            return _sourceChannelDistributors;
        }

        private void OnChannelStatusStopped(object? sender, ChannelStatusStopped e)
        {
            if (sender is not null and IChannelStatus channelStatus)
            {
                channelStatus.Shutdown = true;
                //if (channelStatus.Shutdown)
                //{
                channelStatus.Shutdown = true;
                OnChannelStatusStoppedEvent?.Invoke(sender!, e);
                StopAndUnRegisterChannelStatus(e.Id);
                _videoInfoService.RemoveSourceChannel(e.Name);
                //}
            }


        }
    }
}
