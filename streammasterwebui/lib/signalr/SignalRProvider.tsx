import { useSMMessages } from '@lib/redux/hooks/useSMMessages';
import useGetChannelGroups from '@lib/smAPI/ChannelGroups/useGetChannelGroups';
import useGetPagedChannelGroups from '@lib/smAPI/ChannelGroups/useGetPagedChannelGroups';
import useGetEPGColors from '@lib/smAPI/EPG/useGetEPGColors';
import useGetEPGFilePreviewById from '@lib/smAPI/EPGFiles/useGetEPGFilePreviewById';
import useGetEPGFiles from '@lib/smAPI/EPGFiles/useGetEPGFiles';
import useGetEPGNextEPGNumber from '@lib/smAPI/EPGFiles/useGetEPGNextEPGNumber';
import useGetPagedEPGFiles from '@lib/smAPI/EPGFiles/useGetPagedEPGFiles';
import useGetIcons from '@lib/smAPI/Icons/useGetIcons';
import useGetM3UFileNames from '@lib/smAPI/M3UFiles/useGetM3UFileNames';
import useGetPagedM3UFiles from '@lib/smAPI/M3UFiles/useGetPagedM3UFiles';
import useGetSMChannelStreams from '@lib/smAPI/SMChannelStreamLinks/useGetSMChannelStreams';
import useGetPagedSMChannels from '@lib/smAPI/SMChannels/useGetPagedSMChannels';
import useGetSMChannelNames from '@lib/smAPI/SMChannels/useGetSMChannelNames';
import useGetPagedSMStreams from '@lib/smAPI/SMStreams/useGetPagedSMStreams';
import useGetStationChannelNames from '@lib/smAPI/SchedulesDirect/useGetStationChannelNames';
import useGetIsSystemReady from '@lib/smAPI/Settings/useGetIsSystemReady';
import useGetSettings from '@lib/smAPI/Settings/useGetSettings';
import useGetSystemStatus from '@lib/smAPI/Settings/useGetSystemStatus';
import useGetStreamGroupSMChannels from '@lib/smAPI/StreamGroupSMChannelLinks/useGetStreamGroupSMChannels';
import useGetPagedStreamGroups from '@lib/smAPI/StreamGroups/useGetPagedStreamGroups';
import useGetStreamGroups from '@lib/smAPI/StreamGroups/useGetStreamGroups';
import { ClearByTag, FieldData, SMMessage } from '@lib/smAPI/smapiTypes';
import React, { ReactNode, createContext, useCallback, useContext, useEffect } from 'react';
import SignalRService from './SignalRService';

const SignalRContext = createContext<SignalRService | undefined>(undefined);

export const useSignalRService = () => {
  const context = useContext(SignalRContext);
  if (context === undefined) {
    throw new Error('useSignalRService must be used within a SignalRProvider');
  }
  return context;
};

interface SignalRProviderProps {
  children: ReactNode;
}
export const SignalRProvider: React.FC<SignalRProviderProps> = ({ children }) => {
  const smMessages = useSMMessages();
  const signalRService = SignalRService.getInstance();
  const getChannelGroups = useGetChannelGroups();
  const getEPGColors = useGetEPGColors();
  const getEPGFilePreviewById = useGetEPGFilePreviewById();
  const getEPGFiles = useGetEPGFiles();
  const getEPGNextEPGNumber = useGetEPGNextEPGNumber();
  const getIcons = useGetIcons();
  const getIsSystemReady = useGetIsSystemReady();
  const getM3UFileNames = useGetM3UFileNames();
  const getPagedChannelGroups = useGetPagedChannelGroups();
  const getPagedEPGFiles = useGetPagedEPGFiles();
  const getPagedM3UFiles = useGetPagedM3UFiles();
  const getPagedSMChannels = useGetPagedSMChannels();
  const getPagedSMStreams = useGetPagedSMStreams();
  const getPagedStreamGroups = useGetPagedStreamGroups();
  const getSettings = useGetSettings();
  const getSMChannelNames = useGetSMChannelNames();
  const getSMChannelStreams = useGetSMChannelStreams();
  const getStationChannelNames = useGetStationChannelNames();
  const getStreamGroups = useGetStreamGroups();
  const getStreamGroupSMChannels = useGetStreamGroupSMChannels();
  const getSystemStatus = useGetSystemStatus();

  const addMessage = useCallback(
    (entity: SMMessage): void => {
      smMessages.AddMessage(entity);
    },
    [smMessages]
  );

  const dataRefresh = useCallback(
    (entity: string): void => {
      if (entity === 'GetChannelGroups') {
        getChannelGroups.SetIsForced(true);
        return;
      }
      if (entity === 'GetEPGColors') {
        getEPGColors.SetIsForced(true);
        return;
      }
      if (entity === 'GetEPGFilePreviewById') {
        getEPGFilePreviewById.SetIsForced(true);
        return;
      }
      if (entity === 'GetEPGFiles') {
        getEPGFiles.SetIsForced(true);
        return;
      }
      if (entity === 'GetEPGNextEPGNumber') {
        getEPGNextEPGNumber.SetIsForced(true);
        return;
      }
      if (entity === 'GetIcons') {
        getIcons.SetIsForced(true);
        return;
      }
      if (entity === 'GetIsSystemReady') {
        getIsSystemReady.SetIsForced(true);
        return;
      }
      if (entity === 'GetM3UFileNames') {
        getM3UFileNames.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedChannelGroups') {
        getPagedChannelGroups.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedEPGFiles') {
        getPagedEPGFiles.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedM3UFiles') {
        getPagedM3UFiles.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedSMChannels') {
        getPagedSMChannels.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedSMStreams') {
        getPagedSMStreams.SetIsForced(true);
        return;
      }
      if (entity === 'GetPagedStreamGroups') {
        getPagedStreamGroups.SetIsForced(true);
        return;
      }
      if (entity === 'GetSettings') {
        getSettings.SetIsForced(true);
        return;
      }
      if (entity === 'GetSMChannelNames') {
        getSMChannelNames.SetIsForced(true);
        return;
      }
      if (entity === 'GetSMChannelStreams') {
        getSMChannelStreams.SetIsForced(true);
        return;
      }
      if (entity === 'GetStationChannelNames') {
        getStationChannelNames.SetIsForced(true);
        return;
      }
      if (entity === 'GetStreamGroups') {
        getStreamGroups.SetIsForced(true);
        return;
      }
      if (entity === 'GetStreamGroupSMChannels') {
        getStreamGroupSMChannels.SetIsForced(true);
        return;
      }
      if (entity === 'GetSystemStatus') {
        getSystemStatus.SetIsForced(true);
        return;
      }
      if (entity === 'ChannelGroups') {
        getChannelGroups.SetIsForced(true);
        getPagedChannelGroups.SetIsForced(true);
        return;
      }
      if (entity === 'EPG') {
        getEPGColors.SetIsForced(true);
        return;
      }
      if (entity === 'EPGFiles') {
        getEPGFilePreviewById.SetIsForced(true);
        getEPGFiles.SetIsForced(true);
        getEPGNextEPGNumber.SetIsForced(true);
        getPagedEPGFiles.SetIsForced(true);
        return;
      }
      if (entity === 'Icons') {
        getIcons.SetIsForced(true);
        return;
      }
      if (entity === 'Settings') {
        getIsSystemReady.SetIsForced(true);
        getSettings.SetIsForced(true);
        getSystemStatus.SetIsForced(true);
        return;
      }
      if (entity === 'M3UFiles') {
        getM3UFileNames.SetIsForced(true);
        getPagedM3UFiles.SetIsForced(true);
        return;
      }
      if (entity === 'SMChannels') {
        getPagedSMChannels.SetIsForced(true);
        getSMChannelNames.SetIsForced(true);
        return;
      }
      if (entity === 'SMStreams') {
        getPagedSMStreams.SetIsForced(true);
        return;
      }
      if (entity === 'StreamGroups') {
        getPagedStreamGroups.SetIsForced(true);
        getStreamGroups.SetIsForced(true);
        return;
      }
      if (entity === 'SMChannelStreamLinks') {
        getSMChannelStreams.SetIsForced(true);
        return;
      }
      if (entity === 'SchedulesDirect') {
        getStationChannelNames.SetIsForced(true);
        return;
      }
      if (entity === 'StreamGroupSMChannelLinks') {
        getStreamGroupSMChannels.SetIsForced(true);
        return;
      }
    },
    [
      getChannelGroups,
      getEPGColors,
      getEPGFilePreviewById,
      getEPGFiles,
      getEPGNextEPGNumber,
      getIcons,
      getIsSystemReady,
      getM3UFileNames,
      getPagedChannelGroups,
      getPagedEPGFiles,
      getPagedM3UFiles,
      getPagedSMChannels,
      getPagedSMStreams,
      getPagedStreamGroups,
      getSettings,
      getSMChannelNames,
      getSMChannelStreams,
      getStationChannelNames,
      getStreamGroups,
      getStreamGroupSMChannels,
      getSystemStatus
    ]
  );

  const setField = useCallback(
    (fieldDatas: FieldData[]): void => {
      fieldDatas.forEach((fieldData) => {
        if (fieldData.Entity === 'GetChannelGroups') {
          getChannelGroups.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetEPGColors') {
          getEPGColors.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetEPGFilePreviewById') {
          getEPGFilePreviewById.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetEPGFiles') {
          getEPGFiles.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetEPGNextEPGNumber') {
          getEPGNextEPGNumber.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetIcons') {
          getIcons.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetIsSystemReady') {
          getIsSystemReady.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetM3UFileNames') {
          getM3UFileNames.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedChannelGroups') {
          getPagedChannelGroups.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedEPGFiles') {
          getPagedEPGFiles.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedM3UFiles') {
          getPagedM3UFiles.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedSMChannels') {
          getPagedSMChannels.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedSMStreams') {
          getPagedSMStreams.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetPagedStreamGroups') {
          getPagedStreamGroups.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetSettings') {
          getSettings.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetSMChannelNames') {
          getSMChannelNames.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetSMChannelStreams') {
          getSMChannelStreams.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetStationChannelNames') {
          getStationChannelNames.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetStreamGroups') {
          getStreamGroups.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetStreamGroupSMChannels') {
          getStreamGroupSMChannels.SetField(fieldData);
          return;
        }
        if (fieldData.Entity === 'GetSystemStatus') {
          getSystemStatus.SetField(fieldData);
          return;
        }
      });
    },
    [
      getChannelGroups,
      getEPGColors,
      getEPGFilePreviewById,
      getEPGFiles,
      getEPGNextEPGNumber,
      getIcons,
      getIsSystemReady,
      getM3UFileNames,
      getPagedChannelGroups,
      getPagedEPGFiles,
      getPagedM3UFiles,
      getPagedSMChannels,
      getPagedSMStreams,
      getPagedStreamGroups,
      getSettings,
      getSMChannelNames,
      getSMChannelStreams,
      getStationChannelNames,
      getStreamGroups,
      getStreamGroupSMChannels,
      getSystemStatus
    ]
  );

  const clearByTag = useCallback(
    (data: ClearByTag): void => {
      const { Entity, Tag } = data;
      if (Entity === 'GetChannelGroups') {
        getChannelGroups.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetEPGColors') {
        getEPGColors.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetEPGFilePreviewById') {
        getEPGFilePreviewById.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetEPGFiles') {
        getEPGFiles.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetEPGNextEPGNumber') {
        getEPGNextEPGNumber.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetIcons') {
        getIcons.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetIsSystemReady') {
        getIsSystemReady.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetM3UFileNames') {
        getM3UFileNames.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedChannelGroups') {
        getPagedChannelGroups.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedEPGFiles') {
        getPagedEPGFiles.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedM3UFiles') {
        getPagedM3UFiles.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedSMChannels') {
        getPagedSMChannels.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedSMStreams') {
        getPagedSMStreams.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetPagedStreamGroups') {
        getPagedStreamGroups.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetSettings') {
        getSettings.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetSMChannelNames') {
        getSMChannelNames.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetSMChannelStreams') {
        getSMChannelStreams.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetStationChannelNames') {
        getStationChannelNames.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetStreamGroups') {
        getStreamGroups.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetStreamGroupSMChannels') {
        getStreamGroupSMChannels.ClearByTag(Tag);
        return;
      }
      if (Entity === 'GetSystemStatus') {
        getSystemStatus.ClearByTag(Tag);
        return;
      }
      console.log('ClearByTag', Entity, Tag);
    },
    [
      getChannelGroups,
      getEPGColors,
      getEPGFilePreviewById,
      getEPGFiles,
      getEPGNextEPGNumber,
      getIcons,
      getIsSystemReady,
      getM3UFileNames,
      getPagedChannelGroups,
      getPagedEPGFiles,
      getPagedM3UFiles,
      getPagedSMChannels,
      getPagedSMStreams,
      getPagedStreamGroups,
      getSettings,
      getSMChannelNames,
      getSMChannelStreams,
      getStationChannelNames,
      getStreamGroups,
      getStreamGroupSMChannels,
      getSystemStatus
    ]
  );

  const RemoveConnections = useCallback(() => {
    console.log('SignalR RemoveConnections');
    signalRService.removeListener('ClearByTag', clearByTag);
    signalRService.removeListener('SendMessage', addMessage);
    signalRService.removeListener('DataRefresh', dataRefresh);
    signalRService.removeListener('SetField', setField);
  }, [addMessage, clearByTag, dataRefresh, setField, signalRService]);

  const CheckAndAddConnections = useCallback(() => {
    console.log('SignalR AddConnections');
    signalRService.addListener('ClearByTag', clearByTag);
    signalRService.addListener('SendMessage', addMessage);
    signalRService.addListener('DataRefresh', dataRefresh);
    signalRService.addListener('SetField', setField);
  }, [addMessage, clearByTag, dataRefresh, setField, signalRService]);

  useEffect(() => {
    const handleConnect = () => {
      // setIsConnected(true);
      CheckAndAddConnections();
    };
    const handleDisconnect = () => {
      // setIsConnected(false);
      RemoveConnections();
    };

    // Add event listeners
    signalRService.addEventListener('signalr_connected', handleConnect);
    signalRService.addEventListener('signalr_disconnected', handleDisconnect);

    // Remove event listeners on cleanup
    return () => {
      signalRService.removeEventListener('signalr_connected', handleConnect);
      signalRService.removeEventListener('signalr_disconnected', handleDisconnect);
    };
  }, [CheckAndAddConnections, RemoveConnections, signalRService]);

  return <SignalRContext.Provider value={signalRService}>{children}</SignalRContext.Provider>;
};
