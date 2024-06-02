import {configureStore} from '@reduxjs/toolkit'
import postsReducer from './slices/postsSlice'
import selectedPostReducer from './slices/selectedPostSlice'
import modalReducer from './slices/modalSlice'
import userReducer from './slices/userSlice'

const store = configureStore({
  reducer: {
    modal: modalReducer,
    posts: postsReducer,
    selectedPost: selectedPostReducer,
    user: userReducer
  }
})

export default store
