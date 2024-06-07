import {
  View,
  Text,
  SafeAreaView,
  TouchableOpacity,
  FlatList,
  KeyboardAvoidingView,
  TouchableWithoutFeedback,
  Keyboard
} from 'react-native'
import React from 'react'
import ImageView from 'react-native-image-viewing'
import AntDesign from 'react-native-vector-icons/AntDesign'
import {useDispatch, useSelector} from 'react-redux'
import PostItem from '../Post/PostItem'
import ImageFooter from '../../../components/ImageFooter'
import {closeModal} from '../../../redux/slices/modalSlice'
import {
  deselectImg,
  deselectPost
} from '../../../redux/slices/selectedPostSlice'
import {COLORS} from '../../../../constants'
import styles from './archive.style'
import BackButton from '../../../components/BackButton'

const Archive = ({navigation}) => {
  const dispatch = useDispatch()
  const data = useSelector(state => state.posts.archivePost)
  const visible = useSelector(state => state.modal)
  const selectedPost = useSelector(state => state.selectedPost.selectedPost)
  const selectedImg = useSelector(state => state.selectedPost.selectedImg)
  console.log(data)

  const toggleModal = () => {
    dispatch(closeModal())
    dispatch(deselectImg())
    dispatch(deselectPost())
  }

  return (
    <SafeAreaView style={styles.container}>
      <KeyboardAvoidingView style={{flex: 1}}>
        <TouchableWithoutFeedback onPress={Keyboard.dismiss} accessible={false}>
          <View style={styles.header}>
            <BackButton style={{flex: 1}} navigation={navigation} />
            <Text style={styles.title}>Archive Post</Text>
            <View style={{flex: 1}} />
          </View>
        </TouchableWithoutFeedback>

        <View style={styles.content}>
          {data.length > 0 ? (
            <FlatList
              data={data}
              style={styles.postFlatlist}
              keyExtractor={item => item.id}
              renderItem={item => <PostItem data={item} />}
            />
          ) : (
            <Text
              style={{
                flex: 1,
                alignSelf: 'center',
                textAlignVertical: 'center'
              }}>
              No saved post
            </Text>
          )}
        </View>
      </KeyboardAvoidingView>
      <ImageView
        images={visible ? selectedPost.imgs : null}
        imageIndex={selectedImg}
        presentationStyle="fullScreen"
        visible={visible}
        onRequestClose={toggleModal}
        FooterComponent={() => <ImageFooter data={selectedPost} />}
      />
    </SafeAreaView>
  )
}

export default Archive
