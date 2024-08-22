﻿using StreamMaster.Streams.Domain.Helpers;
using StreamMaster.Streams.Domain.Statistics;
using StreamMaster.Streams.Services;

using System.Diagnostics;
using System.Threading.Channels;

namespace StreamMaster.Streams.Clients;

public sealed class ClientReadStream : Stream, IClientReadStream
{
    private readonly CancellationTokenSource _readCancel = new();
    private readonly ILogger<ClientReadStream> logger;
    //private long totalBytesRead = 0;
    //private double totalLatency = 0;
    //private int readOperations = 0;
    //private readonly DateTime startTime;
    private readonly MetricsService MetricsService = new();

    public ClientReadStream(ILoggerFactory loggerFactory, string UniqueRequestId)
    {
        logger = loggerFactory.CreateLogger<ClientReadStream>();

        this.UniqueRequestId = UniqueRequestId;
        Channel = ChannelHelper.GetChannel();
        //startTime = DateTime.UtcNow;
        logger.LogInformation("New client read stream for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
    }

    //public StreamHandlerMetrics Metrics => new()
    //{
    //    BytesRead = GetBytesRead(),
    //    Kbps = GetKbps(),
    //    StartTime = GetStartTime(),
    //    AverageLatency = GetAverageLatency()
    //};
    public StreamHandlerMetrics Metrics => MetricsService.Metrics;

    public Channel<byte[]> Channel { get; }

    private bool IsCancelled { get; set; }

    private string UniqueRequestId { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => throw new NotSupportedException();
    public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

    public override void Flush() { }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (IsCancelled)
        {
            return 0;
        }

        int bytesRead = 0;
        try
        {
            bytesRead = await ReadAsync(buffer.AsMemory(), cancellationToken);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogInformation(ex, "Read Task ended for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading buffer for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
        }

        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }
    public override int Read(byte[] buffer, int offset, int count)
    {
        return 0;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        ArgumentNullException.ThrowIfNull(buffer);

        if (offset < 0 || offset >= buffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), "Offset is out of range.");
        }

        if (count < 0 || offset + count > buffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count is out of range.");
        }

        byte[] dataToWrite = new byte[count];
        Array.Copy(buffer, offset, dataToWrite, 0, count);

        try
        {
            // Writing the data to the channel
            Channel.Writer.TryWrite(dataToWrite);
        }
        catch (ChannelClosedException ex)
        {
            logger.LogError(ex, "Attempted to write to a closed channel for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
            throw new InvalidOperationException("The channel is closed and cannot accept writes.", ex);
        }
    }
    public void Cancel()
    {
        IsCancelled = true;
    }
    private bool _disposed = false; // To track whether Dispose has been called

    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
            }

            // Dispose unmanaged resources here if any

            _disposed = true;
        }

        // Call the base class implementation of Dispose
        base.Dispose(disposing);
    }

    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Finalizer in case Dispose wasn't called
    ~ClientReadStream()
    {
        Dispose(false);
    }
    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
    {
        if (IsCancelled)
        {
            return 0;
        }

        Stopwatch stopWatch = Stopwatch.StartNew();

        int bytesRead = 0;

        try
        {
            CancellationTokenSource timedToken = new(TimeSpan.FromSeconds(30));
            using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_readCancel.Token, timedToken.Token, cancellationToken);

            byte[] read = await Channel.Reader.ReadAsync(linkedCts.Token);
            bytesRead = read.Length;

            if (bytesRead == 0)
            {
                return 0;
            }
            read[..bytesRead].CopyTo(buffer);
            // Update statistics
            //totalBytesRead += bytesRead;
            //readOperations++;

            if (timedToken.IsCancellationRequested)
            {
                logger.LogWarning("ReadAsync timedToken cancelled for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
                return bytesRead;
            }
        }
        catch (ChannelClosedException ex)
        {
            logger.LogInformation(ex, "ReadAsync closed for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogInformation(ex, "ReadAsync cancelled ended for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
            logger.LogInformation("ReadAsync {_readCancel.Token}", _readCancel.Token.IsCancellationRequested);
            logger.LogInformation("ReadAsync {cancellationToken}", cancellationToken.IsCancellationRequested);
            bytesRead = 1;
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex, "ReadAsync {cancellationToken}", cancellationToken.IsCancellationRequested);
            bytesRead = 1;
        }
        finally
        {
            stopWatch.Stop();
            //totalLatency += stopWatch.Elapsed.TotalMilliseconds;
            MetricsService.RecordMetrics(bytesRead, stopWatch.Elapsed.TotalMilliseconds);
            if (bytesRead == 0)
            {
                logger.LogDebug("Read 0 bytes for UniqueRequestId: {UniqueRequestId}", UniqueRequestId);
            }
        }

        return bytesRead;
    }

    //public double GetAverageLatency()
    //{
    //    return readOperations == 0 ? 0 : totalLatency / readOperations;
    //}

    //public long GetBytesRead()
    //{
    //    return totalBytesRead;
    //}

    //public double GetKbps()
    //{
    //    TimeSpan elapsed = DateTime.UtcNow - startTime;
    //    if (elapsed.TotalSeconds == 0)
    //    {
    //        return 0;
    //    }
    //    return totalBytesRead * 8 / elapsed.TotalSeconds / 1000; // bytes to kilobits
    //}

    //public DateTime GetStartTime()
    //{
    //    return startTime;
    //}
}