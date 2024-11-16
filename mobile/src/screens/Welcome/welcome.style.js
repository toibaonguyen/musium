import {StyleSheet} from 'react-native'
import {COLORS} from '../../../constants/theme'

const styles = StyleSheet.create({
  img: {
    height: 100,
    width: 100,
    borderRadius: 20,
    position: 'absolute'
  },
  headerTxt: ({
    fontSize = 50,
    fontWeight = 800,
    color = COLORS.white
  } = {}) => ({
    fontSize,
    fontWeight,
    color
  }),
  txt: ({fontSize = 16, color = COLORS.white} = {}) => ({
    fontSize,
    color
  }),
  hyperTxt: {
    fontSize: 16,
    color: COLORS.secondary,
    fontWeight: 'bold'
  }
})

export default styles
