import React from 'react'
import {SafeAreaView, View, Text, Pressable, Image} from 'react-native'
import LinearGradient from 'react-native-linear-gradient'
import Button from '../../components/Button'
import {images, COLORS} from '../../../constants'
import styles from './welcome.style'

const Welcome = ({navigation}) => {
  const renderHeroImage = (source, top, left, transform, width, height) => {
    return (
      <Image
        source={source}
        style={{
          ...styles.img,
          top,
          left,
          transform,
          width: width || styles.img.width,
          height: height || styles.img.height
        }}
      />
    )
  }

  const renderText = (text, additionalStyle = {}) => (
    <Text style={{...styles.txt(), ...additionalStyle}}>{text}</Text>
  )

  const renderHeaderText = (text, additionalStyle = {}) => (
    <Text style={{...styles.headerTxt(), ...additionalStyle}}>{text}</Text>
  )

  return (
    <LinearGradient
      colors={[COLORS.secondary, COLORS.primary]}
      style={{flex: 1}}>
      <SafeAreaView style={{flex: 1}}>
        <View>
          {renderHeroImage(images.hero1, 10, 0, [
            {translateX: 20},
            {translateY: 50},
            {rotate: '-15deg'}
          ])}
          {renderHeroImage(images.hero3, -30, 100, [
            {translateX: 50},
            {translateY: 50},
            {rotate: '-5deg'}
          ])}
          {renderHeroImage(images.hero3, 130, -50, [
            {translateX: 50},
            {translateY: 50},
            {rotate: '15deg'}
          ])}
          {renderHeroImage(
            images.hero2,
            110,
            100,
            [{translateX: 50}, {translateY: 50}, {rotate: '-15deg'}],
            200,
            200
          )}
        </View>

        {/* content  */}
        <View
          style={{
            paddingHorizontal: 22,
            position: 'absolute',
            top: 400,
            width: '100%'
          }}>
          {renderHeaderText("Let's Get")}
          {renderHeaderText('Started', {fontSize: 46})}

          <View style={{marginVertical: 22}}>
            {renderText('Connect with each other with chatting', {
              marginVertical: 4
            })}
            {renderText('Calling, Enjoy Safe and private texting')}
          </View>

          <Button
            title="Join Now"
            onPress={() => navigation.navigate('SignUp')}
            style={{marginTop: 22, width: '100%'}}
          />

          <View
            style={{
              flexDirection: 'row',
              marginTop: 12,
              justifyContent: 'center'
            }}>
            {renderText('Already have an account ?')}
            <Pressable onPress={() => navigation.navigate('SignIn')}>
              {renderText('Sign In', {...styles.hyperTxt, marginLeft: 4})}
            </Pressable>
          </View>
        </View>
      </SafeAreaView>
    </LinearGradient>
  )
}

export default Welcome
