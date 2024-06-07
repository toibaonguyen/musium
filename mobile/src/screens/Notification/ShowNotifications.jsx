import {
    KeyboardAvoidingView,
    View,
    Text,
    Image,
    TouchableOpacity,
    Modal,
} from 'react-native'
import React, { useState, useMemo } from 'react'
import { COLORS } from '../../../constants'
import MaterialIcons from "react-native-vector-icons/MaterialIcons"
import AntDesign from "react-native-vector-icons/AntDesign"
import styles from './showNoti.style'

const ShowNotifications = ({ data }) => {
    const CTAButton = ({ title }) => (
        <TouchableOpacity onPress={() => { }} style={{
            borderRadius: 50, borderColor: COLORS.primary, borderWidth: 1,
            paddingHorizontal: 10, paddingVertical: 5, marginTop: 10,
            alignSelf: "flex-start", width: "auto"
        }}>
            <Text style={{ fontSize: 16, color: COLORS.primary }}>{title}</Text>
        </TouchableOpacity>
    )

    const snapPoints = useMemo(() => ['25%', '50%'], [])

    const [bottomSheetVisible, setbottomSheetVisible] = useState(false)

    const openBottomSheet = () => {
        setbottomSheetVisible(true)
    }

    const closeBottomSheet = () => {
        setbottomSheetVisible(false)
    }

    return (
        
        <KeyboardAvoidingView
            style={{
                flexDirection: "row", alignItems: "center", marginVertical: 10,
                justifyContent: "space-between"
            }}>
            <Image
                style={{ height: 50, width: 50, borderRadius: 100 }}
                source={data.item.logo} />
            <View>
                <Text
                    style={{
                        fontSize: 16, color: COLORS.black, width: 240,
                        marginRight: 5
                    }}>
                    {data.item.description}
                </Text>
                {data.item.isNewJob ? (
                    <CTAButton title="View Job" />
                ) : data.item.isAView ? (
                    <CTAButton title="See all views" />
                ) : data.item.isJobAlert ? (
                    <CTAButton title="See 30+ Jobs" />
                ) : data.item.isConnectionAccepted ? (
                    <CTAButton title="message" />
                ) : null}
            </View>

            <View>
                <Text>{data.item.notificationTime}d</Text>
                <TouchableOpacity onPress={openBottomSheet}>
                    <MaterialIcons name="more-vert" size={18} color={COLORS.greyDark} />
                </TouchableOpacity>
                <Modal
                    transparent={true}
                    animationType='slide'
                    visible={bottomSheetVisible}
                    onRequestClose={closeBottomSheet}
                >
                    
                    <View style={styles.modalContainer}>
                        <View style={styles.bottomSheet}>
                            <TouchableOpacity onPress={closeBottomSheet}>
                                <AntDesign name="minus" size={50} color={COLORS.black} />
                            </TouchableOpacity>
                            <Text>Delete Notification</Text>
                            <Text>Turn off this notification type</Text>
                        </View>
                    </View>
                </Modal>
            </View>
        </KeyboardAvoidingView>
    )
}

export default ShowNotifications