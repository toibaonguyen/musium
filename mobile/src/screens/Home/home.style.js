import {StyleSheet} from 'react-native'
import {COLORS} from '../../../constants'

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.greyLight
  },
  header: {
    // flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-around',
    padding: 10,
    borderBottomWidth: 0.3,
    backgroundColor: COLORS.white
  },
  postFlatlist: {
    padding: 0.5,
    // backgroundColor: 'red',
  }
})

export default styles
