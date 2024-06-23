import StandardHeader from '@components/StandardHeader';
import SchedulesDirectHeadendDataSelector from '@components/schedulesDirect/SchedulesDirectHeadendDataSelector';
import { SDIcon } from '@lib/common/icons';
import { useSMContext } from '@lib/signalr/SMProvider';

import { BlockUI } from 'primereact/blockui';

import { memo, useMemo } from 'react';

const SDEditorHeadEndsAndLineUps = () => {
  const { settings } = useSMContext();

  const isSDReady = useMemo((): boolean => {
    return settings.SDSettings?.SDEnabled ?? false;
  }, [settings]);

  const status = useMemo(() => {
    if (isSDReady) {
      return (
        <span>
          Schedules Direct System Status: <span className="text-green-500">Online</span>
        </span>
      );
    }

    return (
      <div>
        Schedules Direct System Status: <span className="text-red-500">Offline</span>
      </div>
    );
  }, [isSDReady]);

  return (
    <BlockUI blocked={!isSDReady}>
      <StandardHeader displayName={status} icon={<SDIcon />}>
        <div className="col-7 m-0 p-0 pr-1">
          <SchedulesDirectHeadendDataSelector />
        </div>
        <div className="col-5 m-0 p-0 border-2 border-round surface-border">
          <div className="flex grid col-12 pl-1 justify-content-start align-items-center m-0 w-full smallpt"></div>
          {/* <SchedulesDirectLineUpsDataSelector id={'SDEditor'} /> */}
        </div>
      </StandardHeader>
    </BlockUI>
  );
};

export default memo(SDEditorHeadEndsAndLineUps);
