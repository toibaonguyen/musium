import {SafeAreaView, Image} from 'react-native'
import React, {useEffect} from 'react'
import AsyncStorage from '@react-native-async-storage/async-storage'
import {useDispatch} from 'react-redux'
import {jwtDecode} from 'jwt-decode'
import {refreshToken} from '../../api/authApi'
import {images} from '../../../constants'
import styles from './splash.style'
import { register } from '../../api/fcmApi'

const Splash = ({navigation}) => {
  const dispatch = useDispatch()

  const getDataStorage = async () => {
    try {
      const user = await AsyncStorage.getItem('userInfo')
      const token = await AsyncStorage.getItem('tokenInfo')
      const fcmToken = await AsyncStorage.getItem('fcmToken')

      return {
        user: user ? JSON.parse(user) : null,
        token: token ? JSON.parse(token) : null,
        fcmToken: fcmToken ? JSON.parse(token) : null
      }
    } catch (err) {
      console.log(err)
      return {user: null, token: null}
    }
  }

  const checkTokenExpired = async () => {
    try {
      const {user, token, fcmToken} = await getDataStorage()

      if (!token) {
        navigation.replace('Welcome')
        return
      }

      if (user) {
        const res = await refreshToken(user.id, token.refreshToken)
        await register(token, fcmToken)

        if (res.status === 200 && res.data) {
          await AsyncStorage.setItem('userInfo', JSON.stringify(res.data.user))
          await AsyncStorage.setItem('tokenInfo', JSON.stringify(res.data.data))
          navigation.replace('BottomNavigator')
        } else navigation.replace('Welcome')
      }
    } catch (error) {
      console.log(error)
      navigation.replace('Welcome')
    }
  }

  useEffect(() => {
    checkTokenExpired()
  }, [])

  return (
    <SafeAreaView style={styles.splashContainer}>
      <Image source={images.favicon} />
    </SafeAreaView>
  )
}

export default Splash
