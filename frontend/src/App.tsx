import {GestureHandlerRootView} from 'react-native-gesture-handler'
import React from 'react'
import {Provider} from 'react-redux'
import {NavigationContainer} from '@react-navigation/native'
import RootNavigation from './navigators/RootNavigation'
import store from './redux/store'

const App = () => {
  return (
    <GestureHandlerRootView style={{flex: 1}}>
      <Provider store={store}>
        <NavigationContainer>
          <RootNavigation />
        </NavigationContainer>
      </Provider>
    </GestureHandlerRootView>
  )
}

export default App
