import {
  Image,
  KeyboardAvoidingView,
  SafeAreaView,
  ScrollView,
  Text,
  TouchableOpacity,
  TouchableWithoutFeedback,
  View
} from 'react-native'
import React, {useState} from 'react'
import Share from 'react-native-share'
import AntDesign from 'react-native-vector-icons/AntDesign'
import Feather from 'react-native-vector-icons/Feather'
import Entypo from 'react-native-vector-icons/Entypo'
import FontAwesome from 'react-native-vector-icons/FontAwesome'
import {useNavigation} from '@react-navigation/native'
import {
  Icon,
  IconButton,
  MD3LightTheme,
  Menu,
  TextInput,
  useTheme
} from 'react-native-paper'
import BackButton from '../../../components/BackButton'
import {COLORS, images} from '../../../../constants'
import styles from './postDetail.style'
import ImageGrid from '../../../components/ImageGrid'
import {
  deselectImg,
  deselectPost,
  selectImg,
  selectPost
} from '../../../redux/slices/selectedPostSlice'
import {closeModal, openModal} from '../../../redux/slices/modalSlice'
import {useDispatch, useSelector} from 'react-redux'
import ImageView from 'react-native-image-viewing'
import ImageFooter from '../../../components/ImageFooter'
import {getImageBase64Array} from '../../../utils/imgsUtils'
import CommentItem from './CommentItem'

const PostDetail = ({route}) => {
  const navigation = useNavigation()
  const theme = useTheme()
  const dispatch = useDispatch()
  const data = route.params

  const [visible, setVisible] = useState(false)
  const [isLike, setIsLike] = useState(false)
  const [comment, setComment] = useState('')

  const modalVisible = useSelector(state => state.modal)
  const selectedPost = useSelector(state => state.selectedPost.selectedPost)
  const selectedImg = useSelector(state => state.selectedPost.selectedImg)

  const openMenu = () => setVisible(true)
  const closeMenu = () => setVisible(false)

  const toggleModal = () => {
    dispatch(closeModal())
    dispatch(deselectImg())
    dispatch(deselectPost())
  }

  const handleLike = () => {
    setIsLike(!isLike)
  }

  const handleShare = async () => {
    const {text, imgs} = data
    console.log(imgs)

    let message = `${text}`

    const imageUrls = imgs.map(img => img.uri)

    const base64Array = await getImageBase64Array(imageUrls)
    if (base64Array.length > 0) {
      const options = {
        title: 'Share post via',
        subject: 'Share post via',
        message: message,
        urls: base64Array,
        showAppsToView: true
      }
      try {
        const shareResponse = await Share.open(options)
        console.log('Shared successfully:', shareResponse)
      } catch (error) {
        console.log('Error sharing:', error)
      }
    } else {
      console.log('Base64 data array is empty')
    }
  }

  const handleImgPress = (postId, index) => {
    dispatch(selectPost(data))
    dispatch(selectImg(index))
    dispatch(openModal())
    console.log('Selected post ID:', postId, index)
  }

  const handleComment = () => {
    console.log(comment)
  }

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <BackButton navigation={navigation} />
      </View>

      <ScrollView contentContainerStyle={{padding: 10}}>
        <View style={styles.itemHeader}>
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

        <View style={styles.itemContent}>
          <Text style={styles.contentText}>{data.text}</Text>

          <ImageGrid
            data={data.imgs}
            postId={data.id}
            onPress={handleImgPress}
          />

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
                {data.options.comments.length > 0 && (
                  <>
                    <Text style={styles.text}>
                      {data.options.comments.length} comments
                    </Text>
                    {data.options.shares.length > 0 && (
                      <Text style={styles.text}> • </Text>
                    )}
                  </>
                )}
                <Text style={styles.text}>
                  {data.options.shares.length} shares
                </Text>
              </View>
            </View>
          </View>
        </View>

        <View style={styles.itemFooter}>
          <TouchableOpacity
            style={styles.optionTab}
            onPress={() => handleLike()}>
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

        <View>
          <Text>Comments</Text>
          {data.options.comments.length === 0 ? (
            <Text>Be the first to comment</Text>
          ) : (
            <View>
              {data.options.comments.map((item, index) => (
                <CommentItem key={index} data={item} />
              ))}
            </View>
          )}
        </View>
      </ScrollView>

      <KeyboardAvoidingView
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
        keyboardVerticalOffset={Platform.OS === 'ios' ? 64 : 0}>
        <View
          style={{
            flexDirection: 'row',
            alignItems: 'center',
            paddingLeft: 10,
            backgroundColor: theme.colors.surfaceVariant
          }}>
          <Image
            source={images.hero1}
            style={{height: 40, width: 40, borderRadius: 20}}
          />
          <TextInput
            placeholder="Add your comment..."
            value={comment}
            onChangeText={setComment}
            right={
              <TextInput.Icon
                icon="send"
                color={COLORS.primary}
                onPress={() => handleComment()}
              />
            }
            style={{flex: 1}}
            autoFocus
          />
        </View>
      </KeyboardAvoidingView>

      <ImageView
        images={modalVisible ? selectedPost.imgs : null}
        imageIndex={selectedImg}
        presentationStyle="fullScreen"
        visible={modalVisible}
        onRequestClose={toggleModal}
        FooterComponent={() => <ImageFooter data={selectedPost} />}
      />
    </SafeAreaView>
  )
}

export default PostDetail
