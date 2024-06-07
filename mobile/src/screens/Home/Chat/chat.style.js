import {StyleSheet} from 'react-native'
import { COLORS } from '../../../../constants';

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: COLORS.white,
  },
  header:{
    flexDirection: 'row',
    alignItems: 'center',
    gap: 15,
    padding: 10,
    borderBottomWidth: 4,
    borderColor: COLORS.greyLight
  },
  content:{
    paddingTop: 0,
    paddingBottom: 10,
    paddingLeft: 10,
  },
  chatFlatlist:{
    marginBottom: 55,
  }
})

export default styles;