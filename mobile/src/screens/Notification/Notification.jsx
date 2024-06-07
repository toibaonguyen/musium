import {
    SafeAreaView,
    KeyboardAvoidingView,
    TouchableWithoutFeedback,
    Text,
    View,
    Keyboard,
    FlatList} from 'react-native'
  import React, {useState} from 'react'
  import SearchBar from '../../components/SearchBar'
  import styles from './notification.style'
  import NotiData from '../../../assets/data/notiData'
  import ShowNotifications from './ShowNotifications'
  
  const Notification = () => {
    const [searchPhrase, setSearchPhrase] = useState('')
  
    return (
      <SafeAreaView style={styles.container}>
        <KeyboardAvoidingView style={{ flex: 1 }}>
          {/* <TouchableWithoutFeedback onPress={Keyboard.dismiss} accessible={false}> */}
          <TouchableWithoutFeedback accessible={false}>
            <View style={styles.header}>
              <SearchBar
                searchPhrase={searchPhrase}
                setSearchPhrase={setSearchPhrase}
              />
            </View>
          </TouchableWithoutFeedback>
  
          {/* <FlatList
            data={NotiData}
            showsVerticalScrollIndicator = {false}
            renderItem={item => <NotiItem data={item} />}
          /> */}
          <FlatList
            data={NotiData}
            showsVerticalScrollIndicator = {false}
            renderItem={item => <ShowNotifications data = {item}/>}
          />
        </KeyboardAvoidingView>
      </SafeAreaView>
    )
  }
  
  export default Notification