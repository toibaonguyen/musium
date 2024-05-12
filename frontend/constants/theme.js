import {Dimensions} from 'react-native'
const {height, width} = Dimensions.get('window')

export const COLORS = {
  primary: '#007ACC', // Màu xanh dương chủ đạo
  secondary: '#6EB5E5', // Màu xanh dương nhạt, là phiên bản của primary
  accent: '#FF5A5F', // Màu nổi bật, có thể sử dụng cho các yếu tố nhấn
  background: '#E2E8FA', // Màu nền
  text: '#333333', // Màu chữ chính
  textSecondary: '#666666', // Màu chữ phụ
  border: '#DDDDDD', // Màu đường viền
  shadow: 'rgba(0, 0, 0, 0.1)', // Màu bóng
  transparent: 'rgba(0, 0, 0, 0)', // Màu bóng
  black: '#000000', // Màu đen
  white: '#FFFFFF', // Màu trắng
  greyLight: '#EEEEEE', // Màu xám nhạt
  greyMedium: '#999999', // Màu xám trung bình
  greyDark: '#555555' // Màu xám đậm
};


export const SIZES = {
  // global SIZES
  base: 8,
  font: 14,
  radius: 30,
  padding: 10,
  padding2: 12,
  padding3: 16,

  // font sizes
  largeTitle: 50,
  h1: 30,
  h2: 20,
  h3: 18,
  h4: 16,
  body1: 30,
  body2: 20,
  body3: 18,
  body4: 14,
  body5: 12,

  // app dimensions
  width,
  height
}

export const FONTS = {
  largeTitle: {
    fontFamily: 'black',
    fontSize: SIZES.largeTitle,
    lineHeight: 55
  },
  h1: {fontFamily: 'bold', fontSize: SIZES.h1, lineHeight: 36},
  h2: {fontFamily: 'bold', fontSize: SIZES.h2, lineHeight: 30},
  h3: {fontFamily: 'bold', fontSize: SIZES.h3, lineHeight: 22},
  h4: {fontFamily: 'bold', fontSize: SIZES.h4, lineHeight: 20},
  body1: {fontFamily: 'regular', fontSize: SIZES.body1, lineHeight: 36},
  body2: {fontFamily: 'regular', fontSize: SIZES.body2, lineHeight: 30},
  body3: {fontFamily: 'regular', fontSize: SIZES.body3, lineHeight: 22},
  body4: {fontFamily: 'regular', fontSize: SIZES.body4, lineHeight: 20}
}
const appTheme = {COLORS, SIZES, FONTS}
export default appTheme
