import { useClickOutside } from 'primereact/hooks';
import { InputText } from 'primereact/inputtext';
import { type TooltipOptions } from 'primereact/tooltip/tooltipoptions';
import { memo, useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { useDebouncedCallback } from 'use-debounce';
import { v4 as uuidv4 } from 'uuid';
import useScrollAndKeyEvents from '@lib/hooks/useScrollAndKeyEvents';
import { Logger } from '@lib/common/logger';

export interface StringEditorBodyTemplateProperties {
  readonly autoFocus?: boolean;
  readonly darkBackGround?: boolean;
  readonly debounceMs?: number;
  readonly disabled?: boolean;
  readonly disableDebounce?: boolean;
  readonly isLoading?: boolean;
  readonly label?: string;
  readonly labelInline?: boolean;
  readonly onChange?: (value: string | undefined) => void;
  readonly onClick?: () => void;
  readonly onSave?: (value: string | undefined) => void;
  readonly placeholder?: string;
  readonly resetValue?: string | undefined;
  readonly showClear?: boolean;
  readonly tooltip?: string | undefined;
  readonly tooltipOptions?: TooltipOptions | undefined;
  readonly value: string | undefined;
}

const StringEditor: React.FC<StringEditorBodyTemplateProperties> = ({
  autoFocus,
  darkBackGround,
  debounceMs = 1500,
  disabled = false,
  disableDebounce = false,
  isLoading,
  label,
  labelInline = false,
  onChange,
  onClick,
  onSave,
  placeholder,
  showClear = false,
  tooltip,
  tooltipOptions,
  value
}) => {
  const uuid = uuidv4();
  const [isFocused, setIsFocused] = useState<boolean>(false);
  const divReference = useRef<HTMLDivElement | null>(null);
  const [ignoreSave, setIgnoreSave] = useState<boolean>(false);
  const [originalValue, setOriginalValue] = useState<string | null>(null);
  const [inputValue, setInputValue] = useState<string | undefined>('');
  const { code } = useScrollAndKeyEvents();
  const inputRef = useRef<HTMLInputElement>(null);

  const save = useCallback(
    (forceValueSave?: string | undefined) => {
      setIgnoreSave(true);
      Logger.debug('Saving value', { forceValueSave, inputValue });
      onSave && onSave(forceValueSave ?? inputValue);
    },
    [inputValue, onSave]
  );

  const debounced = useDebouncedCallback(
    useCallback(
      (newValue: string) => {
        if (newValue !== originalValue && isLoading !== true) {
          save(newValue);
        }
      },
      [isLoading, originalValue, save]
    ),
    debounceMs
  );

  const needsSave = useMemo(() => inputValue !== '' && originalValue !== inputValue, [inputValue, originalValue]);

  useEffect(() => {
    if (code === 'Enter' || code === 'NumpadEnter') {
      if (needsSave && !ignoreSave) {
        debounced.cancel();
        save();
      }
    }
  }, [code, debounced, ignoreSave, needsSave, save]);

  useClickOutside(divReference, () => {
    if (!isFocused) return;
    setIsFocused(false);
    if (!disableDebounce && originalValue !== inputValue && !ignoreSave) {
      save();
    }
  });

  useEffect(() => {
    if (isLoading !== true && value !== undefined && originalValue !== value) {
      if (originalValue === null) {
        setOriginalValue(value);
        setInputValue(value);
      } else if (value !== inputValue) {
        setInputValue(value);
        setOriginalValue(value);
      }
    } else if (value !== undefined && originalValue !== undefined && originalValue !== '' && value === originalValue && value !== inputValue) {
      // if (disableDebounce) {
      //   setInputValue(inputValue);
      // }
      if (disableDebounce) {
        setInputValue(value);
      }
    }
    setIgnoreSave(false);
  }, [disableDebounce, inputValue, isLoading, originalValue, value]);

  const getDiv = useMemo(() => {
    let ret = 'stringeditorbody-inputtext';
    if (darkBackGround) {
      ret = 'stringeditorbody-inputtext-dark';
    }
    if (needsSave) {
      ret = 'stringeditorbody-inputtext-save';
    }

    if (labelInline) {
      ret += ' w-6';
    }
    return ret;
  }, [labelInline, needsSave, darkBackGround]);

  const doShowClear = useMemo(() => darkBackGround && showClear && isFocused && inputValue !== '', [darkBackGround, inputValue, isFocused, showClear]);

  useEffect(() => {
    if (autoFocus && inputRef.current) {
      inputRef.current.focus();
    }
  }, [autoFocus]);

  return (
    <>
      {label && !labelInline && (
        <>
          <label className="pl-15">{label.toUpperCase()}</label>
          <div className="pt-small" />
        </>
      )}
      <div ref={divReference} className={`flex ${labelInline ? 'align-items-center' : 'flex-column align-items-start'}`}>
        {label && labelInline && <div className="w-6">{label.toUpperCase()}</div>}
        <InputText
          ref={inputRef}
          className={getDiv}
          disabled={disabled}
          id={uuid}
          onChange={(e) => {
            const newValue = e.target.value as string;
            setInputValue(newValue);
            if (!disableDebounce) {
              debounced(newValue);
            } else {
              onChange?.(newValue);
            }
          }}
          onClick={onClick}
          onFocus={() => setIsFocused(true)}
          placeholder={placeholder}
          tooltip={tooltip}
          tooltipOptions={tooltipOptions}
          value={inputValue}
        />
        {doShowClear && (
          <i className="input-icon">
            <i
              className="pi pi-times-circle icon-yellow"
              onClick={() => {
                setInputValue('');
                setOriginalValue('');
                onChange?.('');
              }}
            />
          </i>
        )}
      </div>
    </>
  );
};

StringEditor.displayName = 'String Editor Body Template';

export default memo(StringEditor);
