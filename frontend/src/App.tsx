import {GestureHandlerRootView} from 'react-native-gesture-handler'
import React from 'react'
import Toast from 'react-native-toast-message';
import {Provider} from 'react-redux'
import {NavigationContainer} from '@react-navigation/native'
import RootNavigation from './navigators/RootNavigation'
import toastConfig from './utils/toastConfig';
import store from './redux/store'

const App = () => {
  return (
    <GestureHandlerRootView style={{flex: 1}}>
      <Provider store={store}>
        <NavigationContainer>
          <RootNavigation />
        </NavigationContainer>
      </Provider>
      <Toast config={toastConfig} />
    </GestureHandlerRootView>
  )
}

export default App
