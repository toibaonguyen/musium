import {Image, StyleSheet, Text, View} from 'react-native'
import React from 'react'
import { COLORS } from '../../../../constants'

const CommentItem = ({data}) => {
  return (
    <View style={styles.container}>
      <View style={styles.imgContainer}>
        <Image source={{uri: data.avatar}} style={styles.avaImg} />
      </View>
      <View style={styles.inforContainer}>
        <Text style={styles.textName}>{data.name}</Text>
        <Text style={styles.textComment}>{data.value}</Text>
      </View>
    </View>
  )
}

export default CommentItem

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    gap: 10,
    padding: 5,
    marginBottom: 5,
  },
  imgContainer:{
    width: 50,
    paddingVertical: 10,
  },
  avaImg: {
    height: 50,
    borderRadius: 50
  },
  inforContainer: {
    flex: 1,
    alignItems: 'baseline',
    padding: 10,
    borderRadius: 8,
    backgroundColor: COLORS.greyLight
  },
  textName:{
    color: COLORS.black,
    fontWeight: 'bold',
    fontSize: 15,
  },
  textComment: {
    fontSize: 14
  }
})
