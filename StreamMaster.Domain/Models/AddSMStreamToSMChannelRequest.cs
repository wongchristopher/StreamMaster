﻿namespace StreamMaster.Domain.Models;


public record SMStreamSMChannelRequest(int SMChannelId, string SMStreamId) { }

public record SMChannelRankRequest(int SMChannelId, string SMStreamId, int Rank) { }
