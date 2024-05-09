﻿namespace StreamMaster.Application.StreamGroupSMChannelLinks.Commands;

[SMAPI]
[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public record AddSMChannelToStreamGroupRequest(int StreamGroupId, int SMChannelId) : IRequest<APIResponse>;


internal class AddSMChannelToStreamGroupRequestHandler(IRepositoryWrapper Repository, IDataRefreshService dataRefreshService) : IRequestHandler<AddSMChannelToStreamGroupRequest, APIResponse>
{
    public async Task<APIResponse> Handle(AddSMChannelToStreamGroupRequest request, CancellationToken cancellationToken)
    {
        APIResponse res = await Repository.StreamGroupSMChannelLink.AddSMChannelToStreamGroup(request.StreamGroupId, request.SMChannelId).ConfigureAwait(false);
        if (res.IsError)
        {
            return APIResponse.ErrorWithMessage(res.ErrorMessage);
        }
        //await dataRefreshService.RefreshAllSMChannels().ConfigureAwait(false);

        await dataRefreshService.ClearByTag(SMChannel.MainGet, "notInSG").ConfigureAwait(false);
        await dataRefreshService.ClearByTag(SMChannel.MainGet, "inSG").ConfigureAwait(false);
        await dataRefreshService.RefreshStreamGroupSMChannelLinks().ConfigureAwait(false);

        //StreamGroup? streamGroup = Repository.StreamGroup.GetStreamGroup(request.StreamGroupId);
        ////await hubContext.Clients.All.SetField([fd]).ConfigureAwait(false);
        //if (streamGroup != null)
        //{
        //    //DataResponse<List<SMStreamDto>> streams = await Sender.Send(new UpdateStreamRanksRequest(channel.Id, channel.SMStreams.Select(a => a.SMStream).ToList()), cancellationToken);

        //    //GetSMChannelStreamsRequest re = new(request.SMChannelId);
        //    //FieldData fd = new("GetSMChannelStreams", re, streams.Data);

        //    await hubContext.Clients.All.DataRefresh("StreamGroupSMChannelLinks").ConfigureAwait(false);
        //}

        return res;
    }
}
