﻿using StreamMaster.Domain.API;
using StreamMaster.Domain.Pagination;

using System.Linq.Expressions;

namespace StreamMaster.Domain.Repository;

public interface ISMChannelsRepository
{
    IQueryable<SMChannel> GetQuery(Expression<Func<SMChannel, bool>> expression, bool tracking = false);
    Task CreateSMChannel(SMChannel sMChannel);
    PagedResponse<SMChannelDto> CreateEmptyPagedResponse();
    Task<PagedResponse<SMChannelDto>> GetPagedSMChannels(QueryStringParameters parameters);
    IQueryable<SMChannel> GetQuery(bool tracking = false);
    List<SMChannelDto> GetSMChannels();
    Task<APIResponse> DeleteSMChannel(int smchannelId);
    Task<List<int>> DeleteSMChannelsFromParameters(QueryStringParameters parameters);
    SMChannel? GetSMChannel(int smchannelId);
    Task<APIResponse> CreateSMChannelFromStream(string streamId);
    Task<APIResponse> DeleteSMChannels(List<int> smchannelIds);
    Task<APIResponse> AddSMStreamToSMChannel(int SMChannelId, string SMStreamId);
    Task<APIResponse> RemoveSMStreamFromSMChannel(int SMChannelId, string SMStreamId);
    Task<APIResponse> SetSMStreamRanks(List<SMChannelRankRequest> request);
    Task<APIResponse> SetSMChannelLogo(int SMChannelId, string logo);
    Task<APIResponse> SetSMChannelChannelNumber(int sMChannelId, int channelNumber);
    Task<APIResponse> SetSMChannelName(int sMChannelId, string name);
    Task<APIResponse> SetSMChannelEPGID(int sMChannelId, string EPGId);
}