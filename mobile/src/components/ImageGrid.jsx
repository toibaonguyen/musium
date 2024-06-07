import React from 'react'
import {
  StyleSheet,
  Text,
  View,
  TouchableOpacity,
  Image,
} from 'react-native'

const ImageGrid = ({data, postId, onPress}) => {
  const countFrom = 5
  const conditionalRender = false

  const clickEventListener = index => {
    onPress(postId, index);
  }

  const renderOne = () => {
    return (
      <View style={styles.row}>
        <TouchableOpacity
          style={[styles.imageContent, styles.imageContent1]}
          onPress={() => clickEventListener(0)}>
          <Image style={styles.image} source={{uri: data[0].uri}} />
        </TouchableOpacity>
      </View>
    )
  }

  const renderTwo = () => {
    const conditionalRender =
      [3, 4].includes(data.length) ||
      (data.length > +countFrom && [3, 4].includes(+countFrom))

    return (
      <View style={styles.row}>
        <TouchableOpacity
          style={[styles.imageContent, styles.imageContent2]}
          onPress={() => clickEventListener(conditionalRender ? 1 : 0)}>
          <Image
            style={styles.image}
            source={{uri: conditionalRender ? data[1].uri : data[0].uri}}
          />
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.imageContent, styles.imageContent2]}
          onPress={() => clickEventListener(conditionalRender ? 2 : 1)}>
          <Image
            style={styles.image}
            source={{uri: conditionalRender ? data[2].uri : data[1].uri}}
          />
        </TouchableOpacity>
      </View>
    )
  }

  const renderThree = () => {
    const overlay =
      !countFrom ||
      countFrom > 5 ||
      (data.length > countFrom && [4, 5].includes(+countFrom))
        ? renderCountOverlay(true)
        : renderOverlay()
    const conditionalRender =
      data.length == 4 || (data.length > +countFrom && +countFrom == 4)

    return (
      <View style={styles.row}>
        <TouchableOpacity
          style={[styles.imageContent, styles.imageContent3]}
          onPress={() => clickEventListener(conditionalRender ? 1 : 2)}>
          <Image
            style={styles.image}
            source={{uri: conditionalRender ? data[1].uri : data[2].uri}}
          />
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.imageContent, styles.imageContent3]}
          onPress={() => clickEventListener(conditionalRender ? 2 : 3)}>
          <Image
            style={styles.image}
            source={{uri: conditionalRender ? data[2].uri : data[3].uri}}
          />
        </TouchableOpacity>
        {overlay}
      </View>
    )
  }

  const renderOverlay = () => {
    return (
      <TouchableOpacity
        style={[styles.imageContent, styles.imageContent3]}
        onPress={() => clickEventListener(data.length - 1)}>
        <Image style={styles.image} source={{uri: data[data.length - 1].uri}} />
      </TouchableOpacity>
    )
  }

  const renderCountOverlay = more => {
    const extra = data.length - (countFrom && countFrom > 5 ? 5 : countFrom)
    const conditionalRender =
      data.length == 4 || (data.length > +countFrom && +countFrom == 4)

    return (
      <TouchableOpacity
        style={[styles.imageContent, styles.imageContent3]}
        onPress={() => clickEventListener(conditionalRender ? 3 : 4)}>
        <Image
          style={styles.image}
          source={{uri: conditionalRender ? data[3].uri : data[4].uri}}
        />
        <View style={styles.overlayContent}>
          <View>
            <Text style={styles.count}>+{extra}</Text>
          </View>
        </View>
      </TouchableOpacity>
    )
  }

  const imagesToShow = [...data]

  if (countFrom && data.length > countFrom) {
    imagesToShow.length = countFrom
  }

  return (
    <View style={styles.container}>
      {[1, 3, 4].includes(imagesToShow.length) && renderOne()}
      {imagesToShow.length >= 2 && imagesToShow.length != 4 && renderTwo()}
      {imagesToShow.length >= 4 && renderThree()}
    </View>
  )
}

export default ImageGrid

const styles = StyleSheet.create({
  container: {
    flex: 1,
    marginVertical: 12
  },
  row: {
    flexDirection: 'row'
  },
  imageContent: {
    borderWidth: 1,
    borderColor: 'black',
    height: 120
  },
  imageContent1: {
    width: '100%'
  },
  imageContent2: {
    width: '50%'
  },
  imageContent3: {
    width: '33.33%'
  },
  image: {
    width: '100%',
    height: '100%'
  },
  //overlay efect
  overlayContent: {
    flex: 1,
    position: 'absolute',
    zIndex: 100,
    right: 0,
    width: '100%',
    height: '100%',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    justifyContent: 'center',
    alignItems: 'center'
  },
  count: {
    fontSize: 32,
    color: '#ffffff',
    fontWeight: 'bold',
    textShadowColor: 'rgba(0, 0, 139, 1)',
    textShadowOffset: {width: -1, height: 1},
    textShadowRadius: 10
  }
})
