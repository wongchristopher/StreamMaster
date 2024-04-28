import { useSelectAll } from '@lib/redux/slices/useSelectAll';

import OKButton from '@components/buttons/OKButton';
import XButton from '@components/buttons/XButton';
import { SMCard } from '@components/sm/SMCard';
import { useQueryFilter } from '@lib/redux/slices/useQueryFilter';
import { useSelectedStreamGroup } from '@lib/redux/slices/useSelectedStreamGroup';
import { AutoSetSMChannelNumbers } from '@lib/smAPI/StreamGroups/StreamGroupsCommands';
import { AutoSetSMChannelNumbersRequest } from '@lib/smAPI/smapiTypes';
import { Checkbox, CheckboxChangeEvent } from 'primereact/checkbox';
import { Dialog } from 'primereact/dialog';
import { InputNumber } from 'primereact/inputnumber';
import React, { useState } from 'react';
import AutoSetButton from '../buttons/AutoSetButton';

interface AutoSetSMChannelNumbersProperties {
  label: string;
  readonly onHide?: (didUpload: boolean) => void;
}

const AutoSetSMChannelNumbersDialog = ({ label, onHide }: AutoSetSMChannelNumbersProperties) => {
  const [visible, setVisible] = useState<boolean>(false);

  const { selectedStreamGroup } = useSelectedStreamGroup('StreamGroup');

  const [overwriteNumbers, setOverwriteNumbers] = React.useState<boolean>(true);
  const [startNumber, setStartNumber] = React.useState<number>(1);

  const { selectAll } = useSelectAll('streameditor-SMChannelDataSelector');
  const { queryFilter } = useQueryFilter('streameditor-SMChannelDataSelector');

  const onAutoChannelsSave = React.useCallback(async () => {
    if (!selectedStreamGroup || !queryFilter) {
      return;
    }

    const request = {} as AutoSetSMChannelNumbersRequest;
    request.Parameters = queryFilter;
    request.streamGroupId = selectedStreamGroup.Id;
    request.startingNumber = startNumber;
    request.overWriteExisting = overwriteNumbers;

    AutoSetSMChannelNumbers(request)
      .then(() => {})
      .catch((error) => {
        console.error(error);
      })
      .finally(() => {
        setVisible(false);
      });
  }, [overwriteNumbers, queryFilter, selectedStreamGroup, startNumber]);

  return (
    <>
      <Dialog
        className="p-0 sm-fileupload-header default-border"
        visible={visible}
        style={{ top: '-10%', width: '40vw' }}
        onHide={() => {
          setVisible(false);
        }}
        content={({ hide }) => (
          <SMCard title="Auto Set Channel Numbers" header={<XButton iconFilled={false} onClick={(e) => hide(e)} tooltip="Close" />}>
            <div className="border-1 surface-border flex grid flex-wrap justify-content-center p-0 m-0">
              <div className="flex flex-column mt-2 col-6">
                {`Auto set channel numbers ${overwriteNumbers ? 'and overwrite existing numbers ?' : '?'}`}
                <span className="scalein animation-duration-500 animation-iteration-2 text-bold text-red-500 font-italic mt-2">This will auto save</span>
              </div>

              <div className=" flex mt-2 col-6 align-items-center justify-content-start p-0 m-0">
                <span>
                  <div className="flex col-12 justify-content-center align-items-center p-0 m-0  w-full ">
                    <div className="flex col-2 justify-content-center align-items-center p-0 m-0">
                      <Checkbox
                        checked={overwriteNumbers}
                        id="overwriteNumbers"
                        onChange={(e: CheckboxChangeEvent) => setOverwriteNumbers(e.checked ?? false)}
                      />
                    </div>
                    <span className="flex col-10 text-xs">Overwrite Existing</span>
                  </div>

                  <div className="flex col-12 justify-content-center align-items-center p-0 m-0">
                    <div className="flex col-6 justify-content-end align-items-center p-0 m-0">
                      <span className="text-xs pl-4">Ch. #</span>
                    </div>
                    <div className="flex col-6 pl-1 justify-content-start align-items-center p-0 m-0 w-full">
                      <InputNumber
                        className="numbereditorbody"
                        id="startNumber"
                        max={999_999}
                        min={0}
                        onChange={(e) => e.value && setStartNumber(e.value)}
                        showButtons
                        size={3}
                        value={startNumber}
                      />
                    </div>
                  </div>
                </span>
              </div>
              <div className="flex col-12 gap-2 mt-4 justify-content-center ">
                <OKButton onClick={async () => await onAutoChannelsSave()} />
              </div>
            </div>
          </SMCard>
        )}
      />
      <div className="sm-menu-item flex flex-row align-items-center justify-content-start gap-2" onClick={() => setVisible(true)}>
        <AutoSetButton
          disabled={(selectedStreamGroup !== undefined && selectedStreamGroup.Id === 0) || (selectedStreamGroup === undefined && !selectAll)}
          onClick={() => {}}
          tooltip=""
        />
        {label}
      </div>
    </>
  );
};

AutoSetSMChannelNumbersDialog.displayName = 'Auto Set Channel Numbers';
export default React.memo(AutoSetSMChannelNumbersDialog);
