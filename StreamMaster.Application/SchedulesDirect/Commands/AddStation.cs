﻿using StreamMaster.Application.Settings.CommandsOld;
using StreamMaster.Domain.Configuration;

namespace StreamMaster.Application.SchedulesDirect.Commands;

public record StationRequest(string StationId, string LineUp);

public record AddStation(List<StationRequest> Requests) : IRequest<bool>;

public class AddStationHandler(ILogger<AddStation> logger, IJobStatusService jobStatusService, ISchedulesDirect schedulesDirect, ISender Sender, IOptionsMonitor<SDSettings> intsettings)
: IRequestHandler<AddStation, bool>
{
    private readonly SDSettings sdsettings = intsettings.CurrentValue;

    public async Task<bool> Handle(AddStation request, CancellationToken cancellationToken)
    {
        if (!request.Requests.Any())
        {
            return true;
        }


        if (!sdsettings.SDEnabled)
        {
            return true;
        }

        JobStatusManager jobManager = jobStatusService.GetJobManager(JobType.SDSync, EPGHelper.SchedulesDirectId);

        UpdateSettingRequest updateSettingRequest = new()
        {
            SDSettings = new SDSettingsRequest
            {
                SDStationIds = sdsettings.SDStationIds
            }
        };

        foreach (StationRequest stationRequest in request.Requests)
        {
            StationIdLineup station = new(stationRequest.StationId, stationRequest.LineUp);
            if (updateSettingRequest.SDSettings.SDStationIds.Any(x => x.Lineup == station.Lineup && x.StationId == station.StationId))
            {
                logger.LogInformation("Add Station: Already exists {StationIdLineup}", station.StationId);
                continue;
            }
            logger.LogInformation("Added Station {StationIdLineup}", station.StationId);
            updateSettingRequest.SDSettings.SDStationIds.Add(station);
        }

        _ = await Sender.Send(updateSettingRequest, cancellationToken).ConfigureAwait(false);

        schedulesDirect.ResetCache("SubscribedLineups");
        jobManager.SetForceNextRun();

        //await HubContext.Clients.All.SchedulesDirectsRefresh();
        return true;
    }
}