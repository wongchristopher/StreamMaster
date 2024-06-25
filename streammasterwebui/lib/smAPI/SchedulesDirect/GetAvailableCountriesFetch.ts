import { GetAvailableCountries } from '@lib/smAPI/SchedulesDirect/SchedulesDirectCommands';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { Logger } from '@lib/common/logger';


export const fetchGetAvailableCountries = createAsyncThunk('cache/getGetAvailableCountries', async (_: void, thunkAPI) => {
  try {
    Logger.debug('Fetching GetAvailableCountries');
    const response = await GetAvailableCountries();
    return {param: _, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error', value: undefined });
  }
});


