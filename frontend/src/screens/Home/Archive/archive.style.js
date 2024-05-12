import {StyleSheet} from 'react-native'
import {COLORS} from '../../../../constants'

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.white
  },
  header:{
    flexDirection: 'row',
    alignItems: 'center',
    gap: 15,
    padding: 10,
  },
  title: {
    flex: 1,
    fontSize: 18,
    textAlign: 'center',
    alignSelf: 'flex-end',
    fontWeight: '600',
    color: COLORS.black,
  },
  content:{
    backgroundColor: COLORS.greyLight, 
    flex: 1,
  },
  postFlatlist: {
    padding: 0.5,
    // backgroundColor: 'red',
  }
})

export default styles
