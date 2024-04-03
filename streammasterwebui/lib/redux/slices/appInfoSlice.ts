import { type PayloadAction, createSlice } from '@reduxjs/toolkit';
import { type RootState } from '../store';

interface AppInfoPayload {
  appName?: string;
  isHubConnected: boolean;
  isHubDisconnected: boolean;
}

const initialState: AppInfoPayload = {
  appName: 'Stream Master',
  isHubConnected: false,
  isHubDisconnected: false
};

const appInfoSlice = createSlice({
  initialState,
  name: 'appInfo',
  reducers: {
    setHubConnected: (state, action: PayloadAction<boolean>) => {
      state.isHubConnected = action.payload;
    },
    setHubDisconnected: (state, action: PayloadAction<boolean>) => {
      state.isHubDisconnected = action.payload;
    }
  }
});

export const appInfo = (state: RootState) => state.appInfo;
export const { setHubDisconnected, setHubConnected } = appInfoSlice.actions;
export default appInfoSlice.reducer;
