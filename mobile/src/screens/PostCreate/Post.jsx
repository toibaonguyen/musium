import { StyleSheet, Text, View, Image, SafeAreaView, TextInput, KeyboardAvoidingView, Button, TouchableOpacity, Pressable } from 'react-native'
import React from 'react'
import avt from '../../../assets/images/hero2.jpg'
import { isFulfilled } from '@reduxjs/toolkit';
// import { Button } from 'react-native-paper';

const Post = ({ onPress, title }) => {
  const [value, onChangeText] = React.useState();
  return (
    <SafeAreaView style={{ backgroundColor: 'white', height: '100%' }}>
      <View style={{ flexDirection: 'row' }}>
        <Image
          style={{
            width: 40,
            height: 40,
            margin: 12,
            borderRadius: 70
          }}
          source={avt} />
        <Text style={{ fontSize: 20, color: 'black', textAlignVertical: 'center', marginLeft: 10 }}>Nguyen Van A</Text>
        <Pressable style={{
          alignItems: 'flex-end',
          justifyContent: 'center',
          marginVertical: 10,
          paddingHorizontal: 12,
          borderRadius: 10,
          // elevation: 300,
          marginStart: 130,
          marginRight: 10,
          backgroundColor: '#1AA3FC',
        }} onPress={onPress}>
          <Text style={{
            fontSize: 14,
            // lineHeight: 14,
            fontWeight: 'bold',
            letterSpacing: 0.25,
            color: 'white',
          }}>Post</Text>
        </Pressable>
      </View>

      <KeyboardAvoidingView
        behavior={Platform.OS === 'android' ? 'padding' : 'height'}
        style={{
          backgroundColor: 'white',
          borderBottomColor: '#000000',
          height: '100%'
        }}>
        <TextInput
          editable
          multiline
          inputMode='text'
          placeholder='Share your thoughts...'
          onChangeText={text => onChangeText(text)}
          value={value}
          style={{ paddingLeft: 20 }}
        />

        
      </KeyboardAvoidingView>


    </SafeAreaView>
  )
}
// const styles = StyleSheet.create({
//   input: {
//     height: 500,
//     margin: 12,
//     borderWidth: 1,
//     padding: 10,
//   },
// });
export default Post

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
})