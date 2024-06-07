import {createSlice} from '@reduxjs/toolkit'

const initialState = {
  selectedImg: undefined,
  selectedPost: undefined
}

const selectedPostSlice = createSlice({
  name: 'selectedPost',
  initialState: initialState,
  reducers: {
    selectImg: (state, action) => {
      state.selectedImg = action.payload
    },
    deselectImg: (state, action) => {
      state.selectedImg = undefined
    },
    selectPost: (state, action) => {
      state.selectedPost = action.payload
    },
    deselectPost: state => {
      state.selectedPost = undefined
    }
  }
})

export const {selectImg, deselectImg, selectPost, deselectPost} =
  selectedPostSlice.actions
export default selectedPostSlice.reducer
