import { StyleSheet } from 'react-native'
import { COLORS } from '../../../constants/theme'

export default styles = StyleSheet.create({
    signUpContainer: {
        flex: 1,
        backgroundColor: COLORS.white,
        paddingHorizontal: 22
    },

    labelWrapper: {
        marginVertical: 22
    },

    createAccountLabel: {
        fontSize: 22,
        fontWeight: 'bold',
        marginVertical: 12,
        color: COLORS.black
    },

    connectLabel: {
        fontSize: 16,
        color: COLORS.black
    },

    in4InputWrapper: {
        marginBottom: 12
    },

    inputLabel: {
        fontSize: 16,
        fontWeight: '400',
        marginVertical: 8
    },

    inputWrapper: {
        width: '100%',
        height: 48,
        borderColor: COLORS.black,
        borderWidth: 1,
        borderRadius: 8,
        alignItems: 'center',
        paddingLeft: 22
    },

    input: {
        width: '100%'
    }
})


