import {TouchableOpacity} from 'react-native'
import AntDesign from 'react-native-vector-icons/AntDesign'
import { COLORS } from '../../constants'

const BackButton = ({style, navigation}) => {
  return (
    <TouchableOpacity style={[style]} onPress={() => navigation.goBack()}>
      <AntDesign name="arrowleft" size={24} color={COLORS.black} />
    </TouchableOpacity>
  )
}

export default BackButton
