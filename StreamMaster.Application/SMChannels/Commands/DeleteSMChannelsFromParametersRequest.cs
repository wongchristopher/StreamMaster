﻿using Microsoft.AspNetCore.Http;

namespace StreamMaster.Application.SMChannels.Commands;

[SMAPI]
public record DeleteSMChannelsFromParametersRequest(SMChannelParameters Parameters) : IRequest<DefaultAPIResponse>;

internal class DeleteSMChannelsFromParametersRequestHandler(IRepositoryWrapper Repository, IHubContext<StreamMasterHub, IStreamMasterHub> hubContext, IOptionsMonitor<Setting> settings, IOptionsMonitor<HLSSettings> hlsSettings, IHttpContextAccessor httpContextAccessor) : IRequestHandler<DeleteSMChannelsFromParametersRequest, DefaultAPIResponse>
{
    public async Task<DefaultAPIResponse> Handle(DeleteSMChannelsFromParametersRequest request, CancellationToken cancellationToken)
    {
        List<int> ids = await Repository.SMChannel.DeleteSMChannelsFromParameters(request.Parameters).ConfigureAwait(false);

        if (ids.Count != 0)
        {
            await hubContext.Clients.All.DataRefresh("SMChannelDto").ConfigureAwait(false);
        }

        return APIResponseFactory.Ok;
    }
}
