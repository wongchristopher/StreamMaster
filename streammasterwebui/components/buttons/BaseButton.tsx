import { getLeftToolOptions, getRightToolOptions } from '@lib/common/common';
import { Button } from 'primereact/button';
import { Tooltip } from 'primereact/tooltip';
import React, { CSSProperties, forwardRef, useMemo } from 'react';
import { v4 as uuidv4 } from 'uuid';

export type SeverityType = 'danger' | 'help' | 'info' | 'secondary' | 'success' | 'warning';

export interface BaseButtonProps {
  className?: string;
  disabled?: boolean;
  color?: string;
  icon: string;
  iconFilled?: boolean;
  isLeft?: boolean;
  label?: string;
  onClick: (e: React.SyntheticEvent) => void;
  rounded?: boolean;
  severity?: SeverityType;
  tooltip?: string;
  style?: CSSProperties | undefined;
  outlined?: boolean | undefined;
}

const BaseButton = forwardRef<Button, BaseButtonProps>(
  (
    {
      className: configuredClassName,
      color = 'val(--primary-color-text)',
      disabled = false,
      icon,
      iconFilled = true,
      isLeft = false,
      label,
      style,
      outlined = false,
      onClick,
      rounded = true,
      severity,
      tooltip = '',
      ...props
    },
    ref
  ) => {
    const tooltipClassName = React.useMemo(() => {
      if (iconFilled) {
        return `smbutton-label basebutton-${uuidv4()} ${configuredClassName}`;
      }
      return `smbutton basebutton-${uuidv4()} ${configuredClassName}`;
    }, [configuredClassName, iconFilled]);

    const getStyle = useMemo(() => {
      return {
        ...style,
        color: color
      };
    }, [color, style]);

    return (
      <>
        <Tooltip target={tooltipClassName} />
        <Button
          ref={ref}
          className={tooltipClassName}
          disabled={disabled}
          icon={`pi ${icon}`}
          iconPos="right"
          label={label}
          onClick={onClick}
          outlined={outlined}
          rounded={rounded}
          severity={severity}
          text={!iconFilled}
          tooltip={tooltip}
          tooltipOptions={isLeft ? getLeftToolOptions : getRightToolOptions}
          style={getStyle}
          {...props}
          // pt={{
          //   label: { className: 'text-xs p-0 m-0' }
          // }}
        />
      </>
    );
  }
);

export default BaseButton;
