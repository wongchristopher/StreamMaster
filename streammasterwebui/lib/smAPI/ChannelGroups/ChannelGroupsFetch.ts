import { GetPagedChannelGroups } from '@lib/smAPI/ChannelGroups/ChannelGroupsCommands';
import { createAsyncThunk } from '@reduxjs/toolkit';

export const fetchGetPagedChannelGroups = createAsyncThunk('cache/getGetPagedChannelGroups', async (query: string, thunkAPI) => {
  try {
    console.log('Fetching GetPagedChannelGroups');
    const params = JSON.parse(query);
    const response = await GetPagedChannelGroups(params);
    console.log('Fetched GetPagedChannelGroups ',response?.data.length);
    return { query: query, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error' });
  }
});

