import BaseButton from './BaseButton';
import { ChildButtonProperties } from './ChildButtonProperties';

const UploadButton: React.FC<ChildButtonProperties> = ({ disabled = false, label, outlined, onClick, tooltip = 'Upload' }) => {
  const iconFilled = label ? true : false;

  return (
    <BaseButton
      className={`p-1 px-2 text-xs` + iconFilled ? '' : 'w-2rem'}
      disabled={disabled}
      icon="pi-upload"
      iconFilled={iconFilled}
      onClick={onClick}
      label={label ?? undefined}
      outlined={outlined}
      severity="info"
      tooltip={tooltip}
    />
  );
};

export default UploadButton;
