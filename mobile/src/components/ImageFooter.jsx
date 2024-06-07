import {StyleSheet, Text, View} from 'react-native'
import React from 'react'

const ImageFooter = ({data}) => {
  return (
    <View style={styles.root}>
      <Text
        numberOfLines={2}
        // onTextLayout={onTextLayout}
        style={styles.text}>
        {data.text}
      </Text>
    </View>
  )
}

export default ImageFooter

const styles = StyleSheet.create({
  root: {
    padding: 10,
    paddingBottom: 30,
    // backgroundColor: 'red'
  },
  text: {
    fontSize: 15,
    color: 'white'
  }
})
