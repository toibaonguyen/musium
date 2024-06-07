import React from 'react';
import { BaseToast } from 'react-native-toast-message';

const toastConfig = {
  success: (internalState) => (
    <BaseToast
      {...internalState}
      style={{ borderLeftColor: 'green' }}
      contentContainerStyle={{ paddingHorizontal: 15 }}
      text1Style={{
        fontSize: 15,
        fontWeight: 'bold'
      }}
      text2Style={{
        fontSize: 13,
        color: 'gray',
      }}
      text1NumberOfLines= {2}
      text2NumberOfLines={2}
    />
  ),
  error: (internalState) => (
    <BaseToast
      {...internalState}
      style={{ borderLeftColor: 'red' }}
      contentContainerStyle={{ paddingHorizontal: 15 }}
      text1Style={{
        fontSize: 15,
        fontWeight: 'bold'
      }}
      text2Style={{
        fontSize: 13,
        color: 'gray',
      }}
      text1NumberOfLines= {2}
      text2NumberOfLines={2}
    />
  )
};

export default toastConfig;
