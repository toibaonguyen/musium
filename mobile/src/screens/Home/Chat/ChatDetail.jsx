import {
  View,
  Text,
  TouchableOpacity,
  ActivityIndicator,
  TextInput,
  Image,
  KeyboardAvoidingView
} from 'react-native'
import React, {useCallback, useEffect, useMemo, useRef, useState} from 'react'
import Ionicons from 'react-native-vector-icons/Ionicons'
import {GiftedChat} from 'react-native-gifted-chat'
import ImagePicker from 'react-native-image-crop-picker'
import {
  BottomSheetModal,
  BottomSheetModalProvider,
  BottomSheetBackdrop,
  BottomSheetFlatList
} from '@gorhom/bottom-sheet'
import DocumentPicker from 'react-native-document-picker'
import {getStoragePermissions} from '../../../utils/remoteNotification'
import {CameraRoll} from '@react-native-camera-roll/camera-roll'
import LibraryImageCard from '../../../components/LibraryImageCard'
import styles from './chatDetail.style'
import {images, COLORS} from '../../../../constants'
import { LogBox } from 'react-native'

LogBox.ignoreAllLogs()
const ChatDetail = ({route, navigation}) => {
  const {item} = route.params
  console.log(item)

  const [isLoading, setIsLoading] = useState(false)
  const messageRef = useRef('')
  const [message, setMessage] = useState('')
  const [messages, setMessages] = useState([])
  const [selectedImages, setSelectedImages] = useState([])

  const onSendMessage = useCallback((text = []) => {
    // Kiểm tra xem nội dung tin nhắn có được điền hay không
    if (text) {
      // Tạo một đối tượng tin nhắn
      const newMessage = {
        _id: Math.random().toString(),
        text: text,
        createdAt: new Date(),
        user: {
          _id: 1 // Tin nhắn của phía người nhắn
        }
      }

      // Thêm tin nhắn mới vào danh sách tin nhắn
      setMessages(previousMessages =>
        GiftedChat.append(previousMessages, [newMessage])
      )

      // Xóa nội dung tin nhắn sau khi gửi
      setMessage('')
      messageRef.current.clear()
    }
  }, [])

  useEffect(() => {
    //   setIsLoading(true)
  }, [])

  if (isLoading) {
    return (
      <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
        <ActivityIndicator size={'large'} color={COLORS.primary} />
      </View>
    )
  }

  const renderBackdrop = useCallback(
    props => (
      <BottomSheetBackdrop
        {...props}
        onPress={() => {
          //   dispatch(removeAllSelectedImages())

          imageBottomSheetModalRef.current?.close()
        }}
      />
    ),
    []
  )
  const takePhoto = () => {
    cameraBottomSheetModalRef.current?.close()
    try {
      ImagePicker.openCamera({mediaType: 'photo'}).then(result => {
        console.log(result)
      })
    } catch (err) {
      console.log(err)
    }
  }
  const recordVideo = () => {
    cameraBottomSheetModalRef.current?.close()
    try {
      ImagePicker.openCamera({mediaType: 'video'}).then(result => {
        console.log(result)
      })
    } catch (err) {
      console.log(err)
    }
  }
  const getRecentPhotos = async () => {
    const check = await getStoragePermissions()

    if (check) {
      CameraRoll.getPhotos({
        first: 20,
        assetType: 'All',
        include: ['filename', 'playableDuration']
      })
        .then(r => {
          setRecentPhotos(r.edges)
          // console.log(r.edges)
        })
        .catch(err => {
          console.log(err)
        })
    }
  }
  const pickDocument = async () => {
    try {
      const result = await DocumentPicker.pick({
        type: [DocumentPicker.types.allFiles],
        allowMultiSelection: true,
        quality: 1
      })

      console.log(result)
    } catch (err) {
      if (DocumentPicker.isCancel(err)) {
        // Người dùng đã hủy việc chọn tệp
      } else {
        throw err
      }
    }
  }

  // Bottomsheet variables
  const cameraBottomSheetModalRef = useRef(null)
  const imageBottomSheetModalRef = useRef(null)
  const cameraSnapPoints = useMemo(() => ['15%', '15%'], [])
  const imageSnapPoints = useMemo(() => ['80%', '80%'], [])
  const [recentPhotos, setRecentPhotos] = useState([])

  const handleShowCameraBottomSheet = () => {
    cameraBottomSheetModalRef.current?.present()
  }
  const handleShowImageBottomSheet = () => {
    // dispatch(removeAllSelectedImages())

    getRecentPhotos()

    imageBottomSheetModalRef.current?.present()
  }

  return (
    <BottomSheetModalProvider>
      <KeyboardAvoidingView style={styles.container} enabled>
        <View style={styles.header}>
          <TouchableOpacity
            onPress={() => {
              navigation.goBack()
            }}>
            <Ionicons name={'chevron-back'} size={24} color={COLORS.black} />
          </TouchableOpacity>
          <Image
            source={{uri: item.picture.thumbnail}}
            style={styles.avatarDetail}
          />
          <Text style={styles.nameText}>{item.name.last}</Text>
        </View>

        <GiftedChat
          messages={messages}
          user={{_id: 1}}
          messagesContainerStyle={styles.messagesContainer}
          renderInputToolbar={props => (
            <View style={styles.bottomContainer}>
              <View style={styles.cameraContainer}>
                <TouchableOpacity onPress={handleShowCameraBottomSheet}>
                  <Image source={images.camera} style={styles.cameraImg} />
                </TouchableOpacity>
              </View>

              <View style={styles.inputContainer}>
                <TextInput
                  style={styles.input}
                  ref={messageRef}
                  placeholder="Type your message..."
                  onChangeText={text => setMessage(text)}
                  {...props}
                />

                {message ? (
                  <TouchableOpacity onPress={() => onSendMessage(message)}>
                    <Image
                      source={images.send}
                      style={[styles.imgBtn, {marginHorizontal: 5}]}
                    />
                  </TouchableOpacity>
                ) : (
                  <View
                    style={{
                      flexDirection: 'row'
                    }}>
                    <TouchableOpacity onPress={handleShowImageBottomSheet}>
                      <Image
                        source={images.image}
                        size={25}
                        style={[styles.imgBtn, {marginHorizontal: 5}]}
                      />
                    </TouchableOpacity>
                    <TouchableOpacity onPress={pickDocument}>
                      <Image
                        source={images.attach}
                        size={25}
                        style={[styles.imgBtn, {marginHorizontal: 5}]}
                      />
                    </TouchableOpacity>
                  </View>
                )}
              </View>
            </View>
          )}
        />

        <BottomSheetModal
          ref={cameraBottomSheetModalRef}
          index={1}
          snapPoints={cameraSnapPoints}
          // onChange={handleSheetChanges}
          style={styles.bottomSheet}
          backdropProps={renderBackdrop}>
          <View style={styles.bottomSheetItemContainer}>
            <TouchableOpacity
              style={styles.bottomSheetItem}
              onPress={takePhoto}>
              <Ionicons
                name="images-outline"
                size={30}
                color={COLORS.primary}
              />
              <Text style={styles.btnText}>Take Photo</Text>
            </TouchableOpacity>
            <TouchableOpacity
              style={styles.bottomSheetItem}
              onPress={recordVideo}>
              <Ionicons
                name="videocam-outline"
                size={30}
                color={COLORS.primary}
              />
              <Text style={styles.btnText}>Record Video</Text>
            </TouchableOpacity>
          </View>
        </BottomSheetModal>

        <BottomSheetModal
          ref={imageBottomSheetModalRef}
          index={1}
          snapPoints={imageSnapPoints}
          style={styles.bottomSheet}
          backdropComponent={renderBackdrop}>
          <View style={styles.sendBtnContainer}>
            <TouchableOpacity
              onPress={() => imageBottomSheetModalRef.current?.close()}
              style={styles.closeBtn}>
              <Ionicons name="close" color={COLORS.primary} size={32} />
            </TouchableOpacity>

            <View style={styles.allImagesTextContainer}>
              <Text style={styles.allImagesText}>All Images</Text>
            </View>

            <View style={{height: 45, width: 45}}>
              {selectedImages.length !== 0 ? (
                <TouchableOpacity
                  style={styles.sendBtn}
                  onPress={handleSendImages}>
                  <MaterialCommunityIcons
                    name="send-circle"
                    color={COLORS.primary}
                    size={45}
                  />
                </TouchableOpacity>
              ) : null}
            </View>
          </View>

          <BottomSheetFlatList
            data={recentPhotos}
            scrollEnabled={true}
            numColumns={4}
            showsVerticalScrollIndicator={false}
            contentContainerStyle={styles.imageList}
            renderItem={({item, index}) => {
              return <LibraryImageCard key={index} image={item} />
            }}
          />
        </BottomSheetModal>
      </KeyboardAvoidingView>
    </BottomSheetModalProvider>
  )
}

export default ChatDetail
