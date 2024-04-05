import { GetEPGColors } from '@lib/smAPI/EPGFiles/EPGFilesCommands';
import { GetEPGColorsRequest } from '../smapiTypes';
import { createAsyncThunk } from '@reduxjs/toolkit';


export const fetchGetEPGColors = createAsyncThunk('cache/getGetEPGColors', async (_: void, thunkAPI) => {
  try {
    console.log('Fetching GetEPGColors');
    const response = await GetEPGColors();
    console.log('Fetched GetEPGColors',response);
    return { value: response };
  } catch (error) {
    console.error('Failed to fetch', error);
    return thunkAPI.rejectWithValue({ value: undefined, error: error || 'Unknown error' });
  }
});


