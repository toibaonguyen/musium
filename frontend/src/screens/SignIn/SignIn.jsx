import {
  SafeAreaView,
  View,
  Text,
  Image,
  Pressable,
  TextInput,
  TouchableOpacity
} from 'react-native'
import React, {useState} from 'react'
import Ionicons from 'react-native-vector-icons/Ionicons'
import CheckBox from '@react-native-community/checkbox'
import firebase from 'react-native-firebase';
import {useDispatch} from 'react-redux'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { jwtDecode } from 'jwt-decode'
import {login} from '../../api/authApi'
import Button from '../../components/Button'
import {images, COLORS} from '../../../constants'

const SignIn = ({navigation}) => {
  const dispatch = useDispatch()
  const [isPasswordShown, setIsPasswordShown] = useState(false)
  const [isChecked, setIsChecked] = useState(false)
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const handleLogin = async () => {
    const loginResponse = await login(email, password)
    // const decoded = jwtDecode(loginResponse.data)

    if ( loginResponse.status === 200 && loginResponse.data) {
      console.log(JSON.stringify(loginResponse.data.data))
      try {
        await AsyncStorage.setItem('userInfo', JSON.stringify(loginResponse.data.user))
        await AsyncStorage.setItem('tokenInfo', JSON.stringify(loginResponse.data.data))

        navigation.replace('BottomNavigator')
      } catch (err) {
        console.log(err)
      }
    } else {
      Alert.alert(loginResponse.data.toString())
    }
  }

  return (
    <SafeAreaView style={{flex: 1, backgroundColor: COLORS.white}}>
      <View style={{flex: 1, marginHorizontal: 22}}>
        <View style={{marginVertical: 22}}>
          <Text
            style={{
              fontSize: 22,
              fontWeight: 'bold',
              marginVertical: 12,
              color: COLORS.black
            }}>
            Hi Welcome Back ! 👋
          </Text>

          <Text
            style={{
              fontSize: 16,
              color: COLORS.black,
              fontFamily: 'Inter-Regular'
            }}>
            Hello again you have been missed!
          </Text>
        </View>

        <View style={{marginBottom: 12}}>
          <Text
            style={{
              fontSize: 16,
              fontWeight: 400,
              marginVertical: 8
            }}>
            Email{' '}
          </Text>

          <View
            style={{
              width: '100%',
              height: 48,
              borderColor: COLORS.black,
              borderWidth: 1,
              borderRadius: 8,
              alignItems: 'center',
              justifyContent: 'center',
              paddingLeft: 22
            }}>
            <TextInput
              value={email}
              onChangeText={text => setEmail(text)}
              placeholder="Enter your email address"
              placeholderTextColor={COLORS.black}
              keyboardType="email-address"
              style={{
                width: '100%'
              }}
            />
          </View>
        </View>

        <View style={{marginBottom: 12}}>
          <Text
            style={{
              fontSize: 16,
              fontWeight: 400,
              marginVertical: 8
            }}>
            Password
          </Text>

          <View
            style={{
              width: '100%',
              height: 48,
              borderColor: COLORS.black,
              borderWidth: 1,
              borderRadius: 8,
              alignItems: 'center',
              justifyContent: 'center',
              paddingLeft: 22
            }}>
            <TextInput
              value={password}
              onChangeText={text => setPassword(text)}
              placeholder="Enter your password"
              placeholderTextColor={COLORS.black}
              secureTextEntry={isPasswordShown}
              style={{
                width: '100%'
              }}
            />

            <TouchableOpacity
              onPress={() => setIsPasswordShown(!isPasswordShown)}
              style={{
                position: 'absolute',
                right: 12
              }}>
              {isPasswordShown == true ? (
                <Ionicons name="eye-off" size={24} color={COLORS.black} />
              ) : (
                <Ionicons name="eye" size={24} color={COLORS.black} />
              )}
            </TouchableOpacity>
          </View>
        </View>

        <View
          style={{
            flexDirection: 'row',
            marginVertical: 6,
            alignItems: 'center'
          }}>
          <CheckBox
            style={{marginRight: 8}}
            value={isChecked}
            onValueChange={setIsChecked}
            color={isChecked ? COLORS.primary : undefined}
          />

          <Text>Remember me</Text>
        </View>

        <Button
          title="Sign In"
          filled
          style={{
            marginTop: 18,
            marginBottom: 4
          }}
          onPress={handleLogin}
        />

        <View
          style={{
            flexDirection: 'row',
            alignItems: 'center',
            marginVertical: 20
          }}>
          <View
            style={{
              flex: 1,
              height: 1,
              backgroundColor: COLORS.greyLight,
              marginHorizontal: 10
            }}
          />
          <Text style={{fontSize: 14}}>Or</Text>
          <View
            style={{
              flex: 1,
              height: 1,
              backgroundColor: COLORS.greyLight,
              marginHorizontal: 10
            }}
          />
        </View>

        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'center'
          }}>
          <TouchableOpacity
            onPress={() => console.log('Pressed')}
            style={{
              flex: 1,
              alignItems: 'center',
              justifyContent: 'center',
              flexDirection: 'row',
              height: 52,
              borderWidth: 1,
              borderColor: COLORS.greyLight,
              marginRight: 4,
              borderRadius: 10
            }}>
            <Image
              source={images.facebook}
              style={{
                height: 36,
                width: 36,
                marginRight: 8
              }}
              resizeMode="contain"
            />

            <Text>Facebook</Text>
          </TouchableOpacity>

          <TouchableOpacity
            onPress={() => console.log('Pressed')}
            style={{
              flex: 1,
              alignItems: 'center',
              justifyContent: 'center',
              flexDirection: 'row',
              height: 52,
              borderWidth: 1,
              borderColor: COLORS.greyLight,
              marginRight: 4,
              borderRadius: 10
            }}>
            <Image
              source={images.google}
              style={{
                height: 36,
                width: 36,
                marginRight: 8
              }}
              resizeMode="contain"
            />

            <Text>Google</Text>
          </TouchableOpacity>
        </View>

        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'center',
            marginVertical: 22
          }}>
          <Text style={{fontSize: 16, color: COLORS.black}}>
            Don't have an account ?{' '}
          </Text>
          <Pressable onPress={() => navigation.navigate('SignUp')}>
            <Text
              style={{
                fontSize: 16,
                color: COLORS.primary,
                fontWeight: 'bold',
                marginLeft: 6
              }}>
              Sign Up
            </Text>
          </Pressable>
        </View>
      </View>
    </SafeAreaView>
  )
}

export default SignIn
