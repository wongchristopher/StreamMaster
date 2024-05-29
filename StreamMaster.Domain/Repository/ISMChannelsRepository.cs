﻿using StreamMaster.Domain.API;
using StreamMaster.Domain.Pagination;

using System.Linq.Expressions;

namespace StreamMaster.Domain.Repository;

public interface ISMChannelsRepository : IRepositoryBase<SMChannel>
{
    Task<APIResponse> AddSMStreamToSMChannel(int SMChannelId, string SMStreamId);
    Task<List<FieldData>> AutoSetEPGFromIds(List<int> ids, CancellationToken cancellationToken);
    Task<List<FieldData>> AutoSetEPGFromParameters(QueryStringParameters parameters, CancellationToken cancellationToken);
    Task ChangeGroupName(string oldGroupName, string newGroupName);

    Task<APIResponse> CopySMChannel(int sMChannelId, string newName);

    PagedResponse<SMChannelDto> CreateEmptyPagedResponse();

    Task CreateSMChannel(SMChannel sMChannel);

    Task<APIResponse> CreateSMChannelFromStream(string streamId);

    Task<APIResponse> CreateSMChannelFromStreamParameters(QueryStringParameters parameters);

    Task<APIResponse> CreateSMChannelFromStreams(List<string> streamIds);

    Task<APIResponse> DeleteSMChannel(int smchannelId);

    Task<APIResponse> DeleteSMChannels(List<int> smchannelIds);

    Task<List<int>> DeleteSMChannelsFromParameters(QueryStringParameters parameters);

    Task<PagedResponse<SMChannelDto>> GetPagedSMChannels(QueryStringParameters parameters);

    new IQueryable<SMChannel> GetQuery(Expression<Func<SMChannel, bool>> expression, bool tracking = false);

    new IQueryable<SMChannel> GetQuery(bool tracking = false);

    SMChannel? GetSMChannel(int smchannelId);
    SMChannel? GetSMChannelFromStreamGroup(int smChannelId, int streamGroupNumber);
    List<SMChannelDto> GetSMChannels();

    Task<List<SMChannel>> GetSMChannelsFromStreamGroup(int streamGroupId);

    Task<APIResponse> RemoveSMStreamFromSMChannel(int SMChannelId, string SMStreamId);

    Task<APIResponse> SetSMChannelChannelNumber(int sMChannelId, int channelNumber);

    Task<APIResponse> SetSMChannelEPGID(int sMChannelId, string EPGId);

    Task<APIResponse> SetSMChannelGroup(int sMChannelId, string group);

    Task<APIResponse> SetSMChannelLogo(int SMChannelId, string logo);

    Task<APIResponse> SetSMChannelName(int sMChannelId, string name);

    Task<APIResponse> SetSMChannelProxy(int sMChannelId, int streamingProxy);

    Task<List<FieldData>> SetSMChannelsLogoFromEPGFromIds(List<int> ids, CancellationToken cancellationToken);

    Task<List<FieldData>> SetSMChannelsLogoFromEPGFromParameters(QueryStringParameters parameters, CancellationToken cancellationToken);

    Task<APIResponse> SetSMStreamRanks(List<SMChannelRankRequest> request);

    Task<List<FieldData>> ToggleSMChannelsVisibleById(List<int> ids, CancellationToken cancellationToken);

    Task<SMChannelDto?> ToggleSMChannelVisibleById(int id, CancellationToken cancellationToken);

    Task<List<FieldData>> ToggleSMChannelVisibleByParameters(QueryStringParameters parameters, CancellationToken cancellationToken);
}