﻿namespace StreamMaster.Application.StreamGroupChannelGroups.Queries;

public record GetStreamGroupsFromChannelGroupsQuery(List<int> channelGroupIds) : IRequest<IEnumerable<StreamGroupDto>>;

[LogExecutionTimeAspect]
internal class GetStreamGroupsFromChannelGroupsQueryHandler(ILogger<GetStreamGroupsFromChannelGroupsQuery> logger, IRepositoryWrapper Repository)
    : IRequestHandler<GetStreamGroupsFromChannelGroupsQuery, IEnumerable<StreamGroupDto>>
{
    public async Task<IEnumerable<StreamGroupDto>> Handle(GetStreamGroupsFromChannelGroupsQuery request, CancellationToken cancellationToken = default)
    {
        List<StreamGroupDto> ret = await Repository.StreamGroupChannelGroup.GetStreamGroupsFromChannelGroups(request.channelGroupIds).ConfigureAwait(false);

        return ret;

    }
}
