﻿using StreamMaster.Domain.API;
using StreamMaster.Domain.Pagination;

using System.Linq.Expressions;

namespace StreamMaster.Domain.Repository;

public interface ISMChannelsRepository
{
    IQueryable<SMChannel> GetQuery(Expression<Func<SMChannel, bool>> expression, bool tracking = false);
    Task CreateSMChannel(SMChannel sMChannel);
    PagedResponse<SMChannelDto>? CreateEmptyPagedResponse();
    Task<PagedResponse<SMChannelDto>> GetPagedSMChannels(QueryStringParameters parameters);
    IQueryable<SMChannel> GetQuery(bool tracking = false);
    List<SMChannelDto> GetSMChannels();
    Task<DefaultAPIResponse> DeleteSMChannel(int smchannelId);
    Task<List<int>> DeleteSMChannelsFromParameters(QueryStringParameters parameters);
    SMChannel? GetSMChannel(int smchannelId);
    Task<DefaultAPIResponse> CreateSMChannelFromStream(string streamId);
    Task<DefaultAPIResponse> DeleteSMChannels(List<int> smchannelIds);
    Task<DefaultAPIResponse> AddSMStreamToSMChannel(int SMChannelId, string SMStreamId);
    Task<DefaultAPIResponse> RemoveSMStreamFromSMChannel(int SMChannelId, string SMStreamId);
    Task<DefaultAPIResponse> SetSMStreamRanks(List<SMChannelRankRequest> request);
    Task<string?> SetSMChannelLogo(int SMChannelId, string logo);
    Task<DefaultAPIResponse> SetSMChannelChannelNumber(int sMChannelId, int channelNumber);
    Task<DefaultAPIResponse> SetSMChannelName(int sMChannelId, string name);
}