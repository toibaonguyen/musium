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
    borderBottomWidth: 1,
    borderColor: COLORS.greyLight
  },
  content:{
    paddingTop: 0,
    paddingBottom: 10,
    paddingLeft: 10,
  },
  itemHeader:{
    flexDirection: 'row',
    alignItems: 'flex-start',
    gap: 10
  },
  avtContainer: {},
  avtImg: {
    height: 55,
    width: 55,
    borderRadius: 100
  },
  infoContainer: {},
  infoName: {
    fontWeight: 'bold',
    fontSize: 16,
    color: COLORS.black
  },
  itemContent: {
    flex: 1,
    paddingTop: 10,
    // flexDirection: 'row',
  },
  contentFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginTop: 10
  },
  contentText: {
    flex: 1,
    textAlign: 'justify',
    fontSize: 15,
    color: COLORS.black
  },
  moreText: {
    alignSelf: 'flex-end',
    fontWeight: 'bold',
    fontSize: 16,
    color: COLORS.greyDark
  },
  text: {
    color: COLORS.greyDark
  },
  itemFooter: {
    borderTopWidth: 0.4,
    borderTopColor: COLORS.greyLight,
    paddingTop: 5,
    flexDirection: 'row',
    justifyContent: 'space-around',
    gap: 10
  },
  optionTab: {
    width: 65,
    alignContent: 'center',
    justifyContent: 'center',
    alignItems: 'center'
  },
  optionText: isLike => ({
    fontSize: 12,
    fontWeight: '700',
    color: isLike ? COLORS.primary : COLORS.greyDark
  })
})

export default styles;