import { GetPagedChannelGroups } from '@lib/smAPI/ChannelGroups/ChannelGroupsCommands';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { Logger } from '@lib/common/logger';


export const fetchGetPagedChannelGroups = createAsyncThunk('cache/getGetPagedChannelGroups', async (query: string, thunkAPI) => {
  try {
    if (query === undefined) return;
    const params = JSON.parse(query);
    const response = await GetPagedChannelGroups(params);
    return { query: query, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error' });
  }
});


