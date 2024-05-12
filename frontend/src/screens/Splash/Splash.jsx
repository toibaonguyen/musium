import {SafeAreaView, Image} from 'react-native'
import React, {useEffect} from 'react'
import styles from './splash.style'
import {images} from '../../../constants'

const Splash = ({navigation}) => {
  useEffect(() => {
    setTimeout(() => {
      navigation.replace('Welcome')
    }, 2000)
  }, [])

  return (
    <SafeAreaView style={styles.splashContainer}>
      <Image source={images.favicon} />
    </SafeAreaView>
  )
}

export default Splash
