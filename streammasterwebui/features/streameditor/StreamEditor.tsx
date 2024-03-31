import { memo } from 'react';
import SMChannelDataSelector from './SMChannelDataSelector';
import SMStreamDataSelector from './SMStreamDataSelector';

const StreamEditor = () => {
  const id = 'streameditor';

  return (
    <div className="flex justify-content-between align-items-center">
      <div className="col-7 m-0 p-0">
        <SMChannelDataSelector id={id} />
      </div>
      <div className="col-5 m-0 p-0 border-left-1 border-50">
        <SMStreamDataSelector id={id} />
      </div>
    </div>
  );
};

StreamEditor.displayName = 'Streams';

export default memo(StreamEditor);
