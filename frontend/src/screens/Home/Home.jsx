import {
  SafeAreaView,
  KeyboardAvoidingView,
  TouchableWithoutFeedback,
  Text,
  View,
  Keyboard,
  FlatList,
  ScrollView,
  Modal,
  Pressable,
  TouchableOpacity,
  Dimensions
} from 'react-native'
import React, {useState} from 'react'
import {useDispatch, useSelector} from 'react-redux'
import MaterialCommunityIcons from 'react-native-vector-icons/MaterialCommunityIcons'
import SearchBar from '../../components/SearchBar'
import PostItem from './Post/PostItem'
import ImageView from 'react-native-image-viewing'
import styles from './home.style'
import PostData from '../../../assets/data/postData'
import {closeModal} from '../../redux/slices/modalSlice'
import {deselectImg, deselectPost} from '../../redux/slices/selectedPostSlice'
import ImageFooter from '../../components/ImageFooter'

const Home = ({navigation}) => {
  const dispatch = useDispatch()
  const [searchQuery, setSearchQuery] = useState('')
  const {width, height} = Dimensions.get('window')
  const visible = useSelector(state => state.modal)
  const selectedPost = useSelector(state => state.selectedPost.selectedPost)
  const selectedImg = useSelector(state => state.selectedPost.selectedImg)

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
            <SearchBar
              placeholder={'Search'}
              searchQuery={searchQuery}
              setSearchQuery={setSearchQuery}
            />

            <TouchableOpacity onPress={() => navigation.navigate('Archive')}>
              <MaterialCommunityIcons name="archive" size={24} />
            </TouchableOpacity>

            <TouchableOpacity onPress={() => navigation.navigate('Chat')}>
              <MaterialCommunityIcons name="facebook-messenger" size={24} />
            </TouchableOpacity>
          </View>
        </TouchableWithoutFeedback>

        <FlatList
          data={PostData}
          style={styles.postFlatlist}
          keyExtractor={item => item.id}
          renderItem={item => <PostItem data={item} />}
        />
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

export default Home
