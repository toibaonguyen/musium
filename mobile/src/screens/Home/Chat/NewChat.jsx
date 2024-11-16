import {
  ActivityIndicator,
  FlatList,
  SafeAreaView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View
} from 'react-native'
import React, {useEffect, useState} from 'react'
import {filter} from 'lodash'
import AntDesign from 'react-native-vector-icons/AntDesign'
import {COLORS} from '../../../../constants'
import {TextInput} from 'react-native-paper'
import ChatItem from './ChatItem'

const API_ENDPOINT = `https://randomuser.me/api/?results=30`
const NewChat = ({type, onClose}) => {
  const [isLoading, setIsLoading] = useState(false)
  const [searchQuery, setSearchQuery] = useState('')
  const [data, setData] = useState([])
  const [error, setError] = useState(null)
  const [fullData, setFullData] = useState([])

  useEffect(() => {
    setIsLoading(true)
    fetchData(API_ENDPOINT)
  }, [])

  const handleSearch = query => {
    setSearchQuery(query)
    const formattedQuery = query.toLowerCase()
    const filteredData = filter(fullData, user => {
      return contains(user, formattedQuery)
    })
    setData(filteredData)
  }

  const contains = ({name}, query) => {
    const {first, last} = name

    if (
      first.toLowerCase()?.includes(query) ||
      last.toLowerCase()?.includes(query)
    ) {
      return true
    }

    return false
  }

  const fetchData = async url => {
    try {
      const response = await fetch(url)
      const json = await response.json()
      console.log(json)
      setData(json.results)

      console.log(json.results)
      setFullData(json.results)
      setIsLoading(false)
    } catch (error) {
      setError(error)
      console.log(error)
    }
  }

  if (isLoading) {
    return (
      <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
        <ActivityIndicator size={'large'} color={COLORS.primary} />
      </View>
    )
  }

  if (error) {
    return (
      <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
        <Text>
          Error in fetching data ... Please check your internet connection!{' '}
        </Text>
      </View>
    )
  }

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.header}>
        <TouchableOpacity style={{flex: 1}} onPress={onClose}>
          <AntDesign name="arrowleft" size={24} color={COLORS.black} />
        </TouchableOpacity>
        <Text style={styles.title}>New message</Text>
        <View style={{flex: 1}} />
      </View>

      <View style={styles.toWrapper}>
        <TextInput
          label={'To: '}
          mode="outlined"
          value={searchQuery}
          onChangeText={query => handleSearch(query)}
          placeholder="Type a name or multiple names"
        />
      </View>

      <View style={{padding: 10, paddingTop: 15, backgroundColor: COLORS.shadow}}>
          <Text style={styles.text}>Suggested</Text>
        </View>

      <View style={styles.content}>
        <FlatList
          data={data}
          style={styles.chatFlatlist}
          keyExtractor={item => item.login.username}
          renderItem={item => <ChatItem type={type} data={item} onPress={() => {}} />}
        />
      </View>
    </SafeAreaView>
  )
}

export default NewChat

const styles = StyleSheet.create({
  container: {
    height: '100%',
    backgroundColor: 'white'
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 10,
    paddingBottom: 12
  },
  title: {
    flex: 1,
    fontSize: 18,
    textAlign: 'center',
    alignSelf: 'flex-end',
    fontWeight: '600',
    color: COLORS.black
  },
  toWrapper: {
    paddingHorizontal: 10,
    backgroundColor: COLORS.shadow,
  },
  text: {
    fontSize: 16,
    color: COLORS.black,
  },

  content: {
    padding: 10,
    marginBottom: 90,
  },
  chatFlatlist:{
    marginBottom: 50,
  }
})
