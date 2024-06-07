import { StyleSheet } from "react-native"
import { COLORS } from "../../../constants"

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: COLORS.grey
      },
      header: {
        // flex: 1,
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