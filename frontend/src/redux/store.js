import {configureStore} from '@reduxjs/toolkit'
import postsReducer from './slices/postsSlice'
import selectedPostReducer from './slices/selectedPostSlice'
import modalReducer from './slices/modalSlice'

const store = configureStore({
  reducer: {
    posts: postsReducer,
    selectedPost: selectedPostReducer,
    modal: modalReducer
  }
})

export default store
