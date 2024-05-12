import {Image, StyleSheet, Text, TouchableHighlight, View} from 'react-native'
import React from 'react'
import {COLORS} from '../../../../constants'
import formatDate from '../../../utils/formatDateTime'

const ChatItem = ({data, type, onPress}) => {
  return (
    <TouchableHighlight underlayColor={'#AAA'} style={styles.wrapper}>
      <View style={styles.container}>
        <View style={styles.avtContainer}>
          <Image
            source={{uri: data.item.picture.thumbnail}}
            style={styles.image}
          />
        </View>

        <View style={styles.contentContainer}>
          <View style={styles.nameContainer}>
            <Text style={styles.textName}>
              {data.item.name.first} {data.item.name.last}{' '}
            </Text>
            {type === 'chat' ? (
              <Text style={styles.textDate}>
                {formatDate(data.item.registered.date)}
              </Text>
            ) : null}
          </View>

          <View style={styles.messContainer}>
            <Text style={styles.textMess}>
              {type === 'chat' ? 'Recent Message' : data.item.email}
            </Text>
            {type === 'chat' ? <Text> 5</Text> : null}
          </View>
        </View>
      </View>
    </TouchableHighlight>
  )
}

export default ChatItem

const styles = StyleSheet.create({
  wrapper:{
    flex: 1,
    backgroundColor: COLORS.white,
    borderBottomColor: 'black',
    marginLeft: 7,
    marginBottom: 5,
    paddingTop: 5,
    justifyContent: 'center',
  },
  container: {
    flex: 1,
    flexDirection: 'row',
  },
  avtContainer: {
    // marginTop: 5,
  },
  image: {
    width: 50,
    height: 50,
    borderRadius: 25
  },
  contentContainer: {
    flex: 1,
    flexDirection: 'column',
    marginLeft: 10,
    gap: 5,
    height: '100%',
    marginBottom: 15,
    paddingRight: 18,
    borderBottomWidth: 0.5,
    borderColor: COLORS.greyMedium
  },
  nameContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between'
  },
  textName: {
    fontSize: 16,
    fontWeight: '600',
    color: COLORS.black
  },
  textDate: {
    fontSize: 13
  },
  messContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between'
  },
  textMess: {
    fontSize: 14
  }
})
