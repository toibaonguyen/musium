import { StyleSheet, Text, View, Image, SafeAreaView } from 'react-native'
import React from 'react'
import avt from '../../../assets/images/hero2.jpg'

const Post = () => {
  return (
    <SafeAreaView>
      <View style={{flexDirection : 'row'}}>
        <Image
        style={{
          width: 60, 
          height: 60,
          margin: 8,
          borderRadius: 70
        }}
        source={avt}/>

        
          <Text style={{fontSize: 20, color: 'black' }}>Nguyen Van A</Text>
      </View>
        
    </SafeAreaView>
  )
}

export default Post

const styles = StyleSheet.create({})