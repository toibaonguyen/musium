import { createSlice } from '@reduxjs/toolkit';

const initialState = false

const modalSlice = createSlice({
  name: 'modal',
  initialState: initialState,
  reducers: {
    openModal: state => true,
    closeModal: state => false,
  },
});

export const { openModal, closeModal } = modalSlice.actions;
export default modalSlice.reducer;
