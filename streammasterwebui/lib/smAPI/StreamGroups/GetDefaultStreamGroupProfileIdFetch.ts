import { GetDefaultStreamGroupProfileId } from '@lib/smAPI/StreamGroups/StreamGroupsCommands';
import { Logger } from '@lib/common/logger';
import { createAsyncThunk } from '@reduxjs/toolkit';


export const fetchGetDefaultStreamGroupProfileId = createAsyncThunk('cache/getGetDefaultStreamGroupProfileId', async (_: void, thunkAPI) => {
  try {
    Logger.debug('Fetching GetDefaultStreamGroupProfileId');
    const response = await GetDefaultStreamGroupProfileId();
    return {param: _, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error', value: undefined });
  }
});


