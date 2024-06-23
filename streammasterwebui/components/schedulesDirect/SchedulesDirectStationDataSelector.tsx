import { useLineUpColumnConfig } from '@components/columns/useLineUpColumnConfig';
import SMDataTable from '@components/smDataTable/SMDataTable';
import { ColumnMeta } from '@components/smDataTable/types/ColumnMeta';
import { compareStationPreviews, findDifferenceStationIdLineUps } from '@lib/common/common';
import { useSelectedItems } from '@lib/redux/hooks/selectedItems';
import { AddStation, RemoveStation } from '@lib/smAPI/SchedulesDirect/SchedulesDirectCommands';
import useGetSelectedStationIds from '@lib/smAPI/SchedulesDirect/useGetSelectedStationIds';
import useGetStationPreviews from '@lib/smAPI/SchedulesDirect/useGetStationPreviews';
import { AddStationRequest, RemoveStationRequest, StationIdLineup, StationPreview, StationRequest } from '@lib/smAPI/smapiTypes';
import { Toast } from 'primereact/toast';
import { memo, useCallback, useEffect, useMemo, useRef, useState } from 'react';

const SchedulesDirectStationDataSelector = () => {
  const toast = useRef<Toast>(null);

  const { selectedItems, setSelectedItems } = useSelectedItems<StationPreview>('SchedulesDirectSchedulesDataSelector');

  // const schedulesDirectGetSelectedStationIdsQuery = useSchedulesDirectGetSelectedStationIdsQuery();
  // const stationPreviews = useSchedulesDirectGetStationPreviewsQuery();

  const schedulesDirectGetSelectedStationIdsQuery = useGetSelectedStationIds();
  const stationPreviews = useGetStationPreviews();
  const [isLoading, setIsLoading] = useState(false);

  const { columnConfig: lineUpColumnConfig } = useLineUpColumnConfig();

  useEffect(() => {
    if (
      schedulesDirectGetSelectedStationIdsQuery.isLoading ||
      schedulesDirectGetSelectedStationIdsQuery.data === undefined ||
      stationPreviews.data === undefined
    ) {
      return;
    }

    const sp = schedulesDirectGetSelectedStationIdsQuery.data
      .map((stationIdLineUp) =>
        stationPreviews.data?.find(
          (stationPreview) => stationPreview.StationId === stationIdLineUp.StationId && stationPreview.Lineup === stationIdLineUp.Lineup
        )
      )
      .filter((station) => station !== undefined) as StationPreview[];

    if (findDifferenceStationIdLineUps(sp, selectedItems).length > 0) {
      setSelectedItems(sp as StationPreview[]);
    }
  }, [
    schedulesDirectGetSelectedStationIdsQuery.data,
    schedulesDirectGetSelectedStationIdsQuery.isLoading,
    selectedItems,
    setSelectedItems,
    stationPreviews.data
  ]);

  const onSave = useCallback(
    (stationIdLineUps: StationIdLineup[]) => {
      if (stationIdLineUps === undefined || schedulesDirectGetSelectedStationIdsQuery.data === undefined) {
        return;
      }

      const { added, removed } = compareStationPreviews(schedulesDirectGetSelectedStationIdsQuery.data, stationIdLineUps);

      if (added.length === 0 && removed.length === 0) {
        return;
      }
      setIsLoading(true);

      if (added !== undefined && added.length > 0) {
        const toSend = {} as AddStationRequest;

        toSend.Requests = added.map((station) => {
          const request: StationRequest = { LineUp: station.lineup, StationId: station.stationId };
          return request;
        });

        AddStation(toSend)
          .then(() => {
            if (toast.current) {
              toast.current.show({
                detail: 'Update Station Ids Successful',
                life: 3000,
                severity: 'success',
                summary: 'Successful'
              });
            }
          })
          .catch(() => {
            if (toast.current) {
              toast.current.show({
                detail: 'Update Station Ids Failed',
                life: 3000,
                severity: 'error',
                summary: 'Error'
              });
            }
          })
          .finally(() => {
            setSelectedItems([] as StationPreview[]);
            setIsLoading(false);
          });
      }

      if (removed !== undefined && removed.length > 0) {
        const toSend = {} as RemoveStationRequest;

        toSend.Requests = removed.map((station) => {
          const request: StationRequest = { LineUp: station.lineup, StationId: station.stationId };
          return request;
        });

        RemoveStation(toSend)
          .then(() => {
            if (toast.current) {
              toast.current.show({
                detail: 'Update Station Ids Successful',
                life: 3000,
                severity: 'success',
                summary: 'Successful'
              });
            }
          })
          .catch(() => {
            if (toast.current) {
              toast.current.show({
                detail: 'Update Station Ids Failed',
                life: 3000,
                severity: 'error',
                summary: 'Error'
              });
            }
          })
          .finally(() => {
            setSelectedItems([] as StationPreview[]);
            setIsLoading(false);
          });
      }
    },
    [schedulesDirectGetSelectedStationIdsQuery.data, setSelectedItems]
  );

  function imageBodyTemplate(data: StationPreview) {
    if (!data?.Logo || data.Logo.URL === '') {
      return <div />;
    }

    return (
      <div className="flex flex-nowrap justify-content-center align-items-center p-0">
        <img loading="lazy" alt={data.Logo.URL ?? 'Logo'} className="max-h-1rem max-w-full p-0" src={`${encodeURI(data.Logo.URL ?? '')}`} />
      </div>
    );
  }

  const columns = useMemo((): ColumnMeta[] => {
    const columnConfigs: ColumnMeta[] = [
      { field: 'StationId', filter: true, header: 'Station Id', sortable: true, width: 40 },
      { bodyTemplate: imageBodyTemplate, field: 'image', fieldType: 'image', width: 12 }
    ];
    // // columnConfigs.push(channelGroupConfig);
    columnConfigs.push(lineUpColumnConfig);
    columnConfigs.push({ field: 'Name', filter: true, sortable: true, width: 80 });
    columnConfigs.push({ field: 'Callsign', filter: true, header: 'Call Sign', sortable: true, width: 80 });
    columnConfigs.push({ field: 'Affiliate', filter: true, sortable: true, width: 80 });

    return columnConfigs;
  }, [lineUpColumnConfig]);

  return (
    <>
      <Toast position="bottom-right" ref={toast} />
      <div className="m3uFilesEditor flex flex-column border-2 border-round surface-border w-full p-0">
        <SMDataTable
          columns={columns}
          dataSource={stationPreviews.data}
          defaultSortField="name"
          emptyMessage="No Line Ups"
          enablePaginator
          headerName="SD Channels"
          id="SchedulesDirectStationDataSelector"
          isLoading={stationPreviews.isLoading || isLoading}
          onSelectionChange={(e) => {
            onSave(e);
          }}
          selectedItemsKey="SchedulesDirectSchedulesDataSelector"
          selectionMode="multiple"
          showSelections
          style={{ height: 'calc(100vh - 100px)' }}
        />
      </div>
    </>
  );
};

SchedulesDirectStationDataSelector.displayName = 'SchedulesDirectStationDataSelector';

export default memo(SchedulesDirectStationDataSelector);
