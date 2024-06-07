import React from 'react'
import {StyleSheet, TextInput, View} from 'react-native'
import Feather from 'react-native-vector-icons/Feather'
import Entypo from 'react-native-vector-icons/Entypo'
import {COLORS} from '../../constants'

const SearchBar = ({
  placeholder,
  searchQuery,
  setSearchQuery,
  onChangeText,
  style
}) => {
  return (
    <View style={[styles.container, style]}>
      <Feather name="search" size={20} color="black" />
      <View style={styles.searchBar}>
        <TextInput
          style={styles.input}
          placeholder={placeholder}
          value={searchQuery}
          onChangeText={query => {
            setSearchQuery(query)
            onChangeText && onChangeText(query)
          }}
        />
      </View>
      {searchQuery !== '' && (
        <Entypo
          name="cross"
          size={20}
          color="black"
          onPress={() => {
            setSearchQuery('')
            onChangeText && onChangeText('')
          }}
        />
      )}
    </View>
  )
}

export default SearchBar

// styles
const styles = StyleSheet.create({
  container: {
    justifyContent: 'flex-start',
    alignItems: 'center',
    flexDirection: 'row',
    width: '70%',
    backgroundColor: COLORS.background,
    borderRadius: 5,
    paddingHorizontal: 10
  },
  searchBar: {
    paddingHorizontal: 5,
    flexDirection: 'row',
    width: '85%',
    alignItems: 'center'
  },
  input: {
    fontSize: 16,
    height: 40,
    width: '100%',
    alignContent: 'center'
  }
})
