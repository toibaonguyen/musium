import {GestureHandlerRootView} from 'react-native-gesture-handler'
import React, { useEffect } from 'react'
import Toast from 'react-native-toast-message'
import {Provider} from 'react-redux'
import {NavigationContainer} from '@react-navigation/native'
import RootNavigation from './navigators/RootNavigation'
import {
  notificationListener,
  requestNotificationPermission
} from './utils/remoteNotification'
import toastConfig from './utils/toastConfig'
import store from './redux/store'

const App = () => {
  useEffect(() => {
    requestNotificationPermission()
    notificationListener()
  }, [])

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
