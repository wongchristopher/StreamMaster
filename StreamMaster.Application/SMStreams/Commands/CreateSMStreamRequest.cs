﻿using System.Collections.Concurrent;

namespace StreamMaster.Application.SMStreams.Commands;

[SMAPI]
[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public record CreateSMStreamRequest(
    string Name,
    int? ChannelNumber,
    string? Group,
    string? Logo,
    string Url
    ) : IRequest<APIResponse>
{ }

[LogExecutionTimeAspect]
public class CreateSMStreamRequestHandler(ILogger<CreateSMStreamRequest> Logger, IMessageService messageService, IDataRefreshService dataRefreshService, IRepositoryWrapper Repository)
    : IRequestHandler<CreateSMStreamRequest, APIResponse>
{
    public async Task<APIResponse> Handle(CreateSMStreamRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Url))
        {
            return APIResponse.NotFound;
        }

        try
        {
            var smStream = new SMStream
            {
                Id = request.Url.ConvertStringToId(),
                IsUserCreated = true,
                Name = request.Name,
                ChannelNumber = request.ChannelNumber ?? 0,
                Group = request.Group ?? "All",
                Logo = request.Logo ?? string.Empty,
                Url = request.Url,
                M3UFileId = -1,
                M3UFileName = "CUSTOM"
            };


            ConcurrentDictionary<string, byte> generatedIdsDict = new();
            foreach (var stream in Repository.SMStream.GetQuery())
            {
                generatedIdsDict.TryAdd(stream.ShortSMStreamId, 0);
            }
            smStream.ShortSMStreamId = UniqueHexGenerator.GenerateUniqueHex(generatedIdsDict);

            Repository.SMStream.Create(smStream);
            await Repository.SaveAsync();

            await dataRefreshService.RefreshSMStreams();
            await messageService.SendSuccess("Stream Added", $"Stream '{request.Name}' added successfully");
            return APIResponse.Success;
        }
        catch (Exception exception)
        {
            await messageService.SendError("Exception adding Stream", exception.Message);
            Logger.LogCritical("Exception adding Stream '{exception}'", exception);
        }
        return APIResponse.NotFound;
    }
}
