﻿namespace StreamMaster.Application.SMChannelStreamLinks.Commands;

[SMAPI]
[TsInterface(AutoI = false, IncludeNamespace = false, FlattenHierarchy = true, AutoExportMethods = false)]
public record RemoveSMStreamFromSMChannelRequest(int SMChannelId, string SMStreamId) : IRequest<APIResponse>;

internal class RemoveSMStreamFromSMChannelRequestHandler(IRepositoryWrapper Repository, ISender Sender, IDataRefreshService dataRefreshService)
    : IRequestHandler<RemoveSMStreamFromSMChannelRequest, APIResponse>
{
    public async Task<APIResponse> Handle(RemoveSMStreamFromSMChannelRequest request, CancellationToken cancellationToken)
    {
        APIResponse res = await Repository.SMChannel.RemoveSMStreamFromSMChannel(request.SMChannelId, request.SMStreamId).ConfigureAwait(false);
        if (res.IsError)
        {
            return APIResponse.ErrorWithMessage(res.ErrorMessage);
        }

        SMChannel? smChannel = Repository.SMChannel.GetSMChannel(request.SMChannelId);
        if (smChannel != null)
        {
            //DataResponse<List<SMStreamDto>> streams = await Sender.Send(new UpdateStreamRanksRequest(smChannel.Id, smChannel.SMStreams.Select(a => a.SMStream.Id).ToList()), cancellationToken);

            //GetSMChannelStreamsRequest re = new(request.Id);
            //List<FieldData> ret = new()
            //{                
            //    new(SMStream.APIName, request.SMStreamId, "ChannelMembership", streams.Data)
            //};

            ////await dataRefreshService.RefreshSMChannelStreamLinks();
            //await dataRefreshService.SetField(ret).ConfigureAwait(false);
            //await dataRefreshService.RefreshSMChannelStreamLinks();

            await dataRefreshService.RefreshSMChannelStreamLinks();
            await dataRefreshService.RefreshSMChannels();
            await dataRefreshService.RefreshSMStreams();
        }

        return res;
    }
}
