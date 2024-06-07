import {createSlice} from '@reduxjs/toolkit'

const initialState = {
  info: undefined,
  features: undefined,
  status: undefined
}

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setInfo: (state, action) => {
      state.info = action.payload
    },
    setFeatures: (state, action) => {
      state.features = action.payload
    },
    setStatus: (state, action) => {
      state.status = action.payload
    }
  }
})

export const {setInfo, setFeatures, setStatus} = userSlice.actions

export default userSlice.reducer
