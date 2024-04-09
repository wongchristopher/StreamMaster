import { GetSMChannelStreams } from '@lib/smAPI/SMChannelStreamLinks/SMChannelStreamLinksCommands';
import { GetSMChannelStreamsRequest } from '../smapiTypes';
import { createAsyncThunk } from '@reduxjs/toolkit';


export const fetchGetSMChannelStreams = createAsyncThunk('cache/getGetSMChannelStreams', async (param: GetSMChannelStreamsRequest, thunkAPI) => {
  try {
    console.log('Fetching GetSMChannelStreams');
    const response = await GetSMChannelStreams(param);
    console.log('Fetched GetSMChannelStreams ',response?.length);
    return {param: param, value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ error: error || 'Unknown error', value: undefined });
  }
});


