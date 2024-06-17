import AsyncStorage from '@react-native-async-storage/async-storage'
import messaging from '@react-native-firebase/messaging'
import {PermissionsAndroid, Platform} from 'react-native'

export const requestIosPermission = async () => {
  const authStatus = await messaging().requestPermission()

  const enabled =
    authStatus === messaging.AuthorizationStatus.AUTHORIZED ||
    authStatus === messaging.AuthorizationStatus.PROVISIONAL

  if (enabled) {
    getFcmToken()
  }
}

export const requestAndroidPermission = async () => {
  if (Platform.Version >= '33') {
    const granted = await PermissionsAndroid.request(
      PermissionsAndroid.PERMISSIONS.POST_NOTIFICATIONS
    )
    if (granted !== PermissionsAndroid.RESULTS.GRANTED) {
      console.warn('Notification permission denied')
    } else getFcmToken()
  }
}

export const requestNotificationPermission = async () => {
  if (Platform.OS === 'android') requestAndroidPermission()
  else requestIosPermission()
}

export const getFcmToken = async () => {
  let fcmToken = await AsyncStorage.getItem('fcmToken')

  if (!fcmToken) {
    try {
      const fcmToken = await messaging().getToken()
      console.log(fcmToken)

      if (fcmToken) {
        await AsyncStorage.setItem('fcmToken', fcmToken)
      }
    } catch (error) {
      console.log(`Can not get fcm token ${error}`)
    }
  }
}

// Lắng nghe thông báo từ server
export const notificationListener = () => {
  // Handle when click on the notification from background app
  messaging().onNotificationOpenedApp(remoteMessage => {
    console.log(
      'Notification caused app to open from background state: ',
      remoteMessage.notification
    )
  })

  // Handle when click on the notification from quit app
  messaging()
    .getInitialNotification()
    .then(remoteMessage => {
      if (remoteMessage)
        console.log(
          'Notification caused app to open from quit state: ',
          remoteMessage.notification
        )
    })
}

export const getStoragePermissions = async () => {
  try {
    if (Platform.Version >= 33) {
      const permissions = [
        PermissionsAndroid.PERMISSIONS.READ_MEDIA_IMAGES,
        PermissionsAndroid.PERMISSIONS.READ_MEDIA_VIDEO,
      ];

      const granted = await PermissionsAndroid.requestMultiple(permissions);

      if (
        granted[PermissionsAndroid.PERMISSIONS.READ_MEDIA_IMAGES] ===
          PermissionsAndroid.RESULTS.GRANTED &&
        granted[PermissionsAndroid.PERMISSIONS.READ_MEDIA_VIDEO] ===
          PermissionsAndroid.RESULTS.GRANTED
      ) {
        return true;
      } else {
        console.log(
          'Permission denied: READ_MEDIA_IMAGES & READ_MEDIA_VIDEO will not work',
        );
        return false;
      }
    } else {
      const permissions = PermissionsAndroid.PERMISSIONS.READ_EXTERNAL_STORAGE;

      const granted = await PermissionsAndroid.request(permissions);

      if (granted === PermissionsAndroid.RESULTS.GRANTED) {
        return true;
      } else {
        console.log('Permission denied: READ_EXTERNAL_STORAGE will not work');
        return false;
      }
    }
  } catch (err) {
    console.log(err);
  }
};