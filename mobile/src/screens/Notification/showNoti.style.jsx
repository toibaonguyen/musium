import { StyleSheet } from "react-native"
import { COLORS } from "../../../constants"

const styles = StyleSheet.create({
    modalContainer: {
        flex: 1,
        justifyContent: 'flex-end',
        alignItems: 'center',
        backgroundColor: 'rgba(0,0,0,0.5)'
    },
    bottomSheet: {
        backgroundColor: COLORS.white,
        padding: 20,
        paddingTop: 0,
        borderTopLeftRadius: 20,
        borderTopRightRadius: 20,
        width: '100%',
        alignItems: 'center'
    }
})

export default styles