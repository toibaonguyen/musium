import {
  Image,
  StyleSheet,
  Text,
  View,
  TouchableOpacity,
  TouchableWithoutFeedback,
  ToastAndroid
} from 'react-native'
import Share from 'react-native-share'
import React, {useCallback, useEffect, useState} from 'react'
import {Menu} from 'react-native-paper'
import {useNavigation} from '@react-navigation/native'
import {useDispatch, useSelector} from 'react-redux'
import AntDesign from 'react-native-vector-icons/AntDesign'
import Entypo from 'react-native-vector-icons/Entypo'
import FontAwesome from 'react-native-vector-icons/FontAwesome'
import {COLORS, images} from '../../../../constants'
import ImageGrid from '../../../components/ImageGrid'
import {selectImg, selectPost} from '../../../redux/slices/selectedPostSlice'
import {openModal} from '../../../redux/slices/modalSlice'
import {addPost} from '../../../redux/slices/postsSlice'
import { getImageBase64Array } from '../../../utils/imgsUtils'

const PostItem = ({data}) => {
  const navigation = useNavigation()
  const dispatch = useDispatch()
  const existingPosts = useSelector(state => state.posts.archivePost)

  const [visible, setVisible] = useState(false)
  const openMenu = () => setVisible(true)
  const closeMenu = () => setVisible(false)
  const [showMoreButton, setShowMoreButton] = useState(false)
  const [textShown, setTextShown] = useState(false)
  const [numLines, setNumLines] = useState(undefined)
  const toggleTextShown = () => setTextShown(!textShown)

  const [isLike, setIsLike] = useState(false)

  const handleLike = () => {
    setIsLike(!isLike)
  }

  const handleShare = async () => {
    const {text, imgs} = data.item
    console.log(imgs)

    let message = `${text}`

    const imageUrls = imgs.map(img => img.uri)

    const base64Array = await getImageBase64Array(imageUrls);
    if (base64Array.length > 0) {
      const options = {
        title: "Share post via",
        subject: "Share post via",
        message: message,
        urls: base64Array,
        showAppsToView: true
      };
      try {
        const shareResponse = await Share.open(options);
        console.log("Shared successfully:", shareResponse);
        
      } catch (error) {
        console.log("Error sharing:", error);
      }
    } else {
      console.log("Base64 data array is empty");
    }
  }

  const handleImgPress = (postId, index) => {
    dispatch(selectPost(data.item))
    dispatch(selectImg(index))
    dispatch(openModal())
    console.log('Selected post ID:', postId, index)
  }

  useEffect(() => {
    setNumLines(textShown ? undefined : 4)
  }, [textShown])

  const onTextLayout = useCallback(
    e => {
      if (e.nativeEvent.lines.length > 4 && !textShown) {
        setShowMoreButton(true)
        setNumLines(4)
      }
    },
    [textShown]
  )

  const onSavePost = () => {
    const existingPost = existingPosts.find(post => post.id === data.item.id)
    if (!existingPost) {
      dispatch(addPost({postType: 'archive', post: data.item}))
      ToastAndroid.show('Đã lưu bài post', ToastAndroid.SHORT)
    } else ToastAndroid.show('Post này đã được lưu rồi', ToastAndroid.SHORT)
    closeMenu()
    console.log(data.item)
  }

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <View style={styles.avtContainer}>
          <Image source={images.hero1} style={styles.avtImg} />
        </View>

        <View style={styles.infoContainer}>
          <Text style={styles.infoName}>Bui Luong Hieu</Text>
          <View>
            <Text style={styles.text}>16h</Text>
          </View>
        </View>

        <View style={{position: 'absolute', right: 5, top: 5, padding: 5}}>
          <Menu
            visible={visible}
            style={{}}
            onDismiss={closeMenu}
            anchor={
              <TouchableOpacity onPress={openMenu}>
                <Entypo
                  name="dots-three-vertical"
                  size={16}
                  color={COLORS.black}
                />
              </TouchableOpacity>
            }>
            <Menu.Item
              leadingIcon="bookmark-outline"
              onPress={() => onSavePost()}
              title="Save"
              titleStyle={{fontSize: 14}}
            />
            <Menu.Item
              leadingIcon="tray-arrow-up"
              onPress={() => {}}
              title="Share via"
              titleStyle={{fontSize: 14}}
            />
            <Menu.Item
              leadingIcon="close-circle"
              onPress={() => {}}
              title={`Unfollow Bui Luong Hieu`}
              titleStyle={{fontSize: 14}}
            />
          </Menu>
        </View>
      </View>

      <View style={styles.content}>
        <TouchableWithoutFeedback
          onPress={() =>
            navigation.navigate('PostDetail', data.item)
          }>
          <Text
            numberOfLines={numLines}
            onTextLayout={onTextLayout}
            style={styles.contentText}>
            {data.item.text}
          </Text>
        </TouchableWithoutFeedback>
        {showMoreButton ? (
          <Text style={styles.moreText} onPress={toggleTextShown}>
            {textShown ? ' See less' : ' See more'}
          </Text>
        ) : null}

        <ImageGrid
          data={data.item.imgs}
          postId={data.item.id}
          onPress={handleImgPress}
        />

        <TouchableWithoutFeedback
          onPress={() =>
            navigation.navigate('PostDetail', data.item)
          }>
          <View style={styles.contentFooter}>
            <View style={{flexDirection: 'row', alignItems: 'center', gap: 5}}>
              <TouchableOpacity
                style={{
                  padding: 2,
                  borderWidth: 1,
                  borderRadius: 20,
                  borderColor: COLORS.background,
                  backgroundColor: COLORS.background
                }}>
                <AntDesign name="like1" size={14} color={COLORS.primary} />
              </TouchableOpacity>
              <Text style={styles.text}>240</Text>
            </View>
            <View style={{flexDirection: 'row', gap: 10}}>
              <View style={{flexDirection: 'row', alignItems: 'center'}}>
                {data.item.options.comments.length > 0 && (
                  <>
                    <Text style={styles.text}>
                      {data.item.options.comments.length} comments
                    </Text>
                    {data.item.options.shares.length > 0 && (
                      <Text style={styles.text}> • </Text>
                    )}
                  </>
                )}
                <Text style={styles.text}>
                  {data.item.options.shares.length} shares
                </Text>
              </View>
            </View>
          </View>
        </TouchableWithoutFeedback>
      </View>

      <View style={styles.footer}>
        <TouchableOpacity style={styles.optionTab} onPress={() => handleLike()}>
          <AntDesign
            name={isLike ? 'like1' : 'like2'}
            size={18}
            color={isLike ? COLORS.primary : COLORS.greyDark}
          />
          <Text style={styles.optionText(isLike)}>
            {isLike ? 'Liked' : 'Like'}
          </Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.optionTab}>
          <FontAwesome name="comment-o" size={18} color={COLORS.greyDark} />
          <Text style={styles.optionText(false)}>Comment</Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={styles.optionTab}
          onPress={() => handleShare()}>
          <FontAwesome name="share" size={18} color={COLORS.greyDark} />
          <Text style={styles.optionText(false)}>Share</Text>
        </TouchableOpacity>

        <TouchableOpacity style={styles.optionTab}>
          <FontAwesome name="send" size={18} color={COLORS.greyDark} />
          <Text style={styles.optionText(false)}>Send</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

export default PostItem

const styles = StyleSheet.create({
  container: {
    marginHorizontal: 5,
    marginVertical: 5,
    padding: 10,
    paddingBottom: 5,
    gap: 10,
    borderRadius: 7,
    backgroundColor: 'white'
  },
  header: {
    flexDirection: 'row',
    alignItems: 'flex-start',
    gap: 10
  },
  avtContainer: {},
  avtImg: {
    height: 55,
    width: 55,
    borderRadius: 100
  },
  infoContainer: {},
  infoName: {
    fontWeight: 'bold',
    fontSize: 16,
    color: COLORS.black
  },
  content: {
    flex: 1
    // flexDirection: 'row',
  },
  contentFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 10
  },
  contentText: {
    flex: 1,
    textAlign: 'justify',
    fontSize: 15,
    color: COLORS.black
  },
  moreText: {
    alignSelf: 'flex-end',
    fontWeight: 'bold',
    fontSize: 16,
    color: COLORS.greyDark
  },
  text: {
    color: COLORS.greyDark
  },
  footer: {
    borderTopWidth: 0.4,
    borderTopColor: COLORS.greyLight,
    paddingTop: 5,
    flexDirection: 'row',
    justifyContent: 'space-around',
    gap: 10
  },
  optionTab: {
    width: 65,
    alignContent: 'center',
    justifyContent: 'center',
    alignItems: 'center'
  },
  optionText: isLike => ({
    fontSize: 12,
    fontWeight: '700',
    color: isLike ? COLORS.primary : COLORS.greyDark
  })
})
