import {createSlice} from '@reduxjs/toolkit'

const initialState = {
  archivePost: [],
  myPost: [],
}

const postsSlice = createSlice({
  name: 'posts',
  initialState,
  reducers: {
    addPost: (state, action) => {
      const { postType, post } = action.payload;
      if (postType === 'archive') {
        state.archivePost.push(post);
      } else if (postType === 'my') {
        state.myPost.push(post);
      }
    },
    deletePost: (state, action) => {
      const { postType, postId } = action.payload;
      if (postType === 'archive') {
        state.archivePost = state.archivePost.filter(post => post.id !== postId);
      } else if (postType === 'my') {
        state.myPost = state.myPost.filter(post => post.id !== postId);
      }
    },
  },
});

export const { addPost, deletePost } = postsSlice.actions;
export default postsSlice.reducer;