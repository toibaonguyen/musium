function formatDate(dateString) {
  const months = [
    'Jan',
    'Feb',
    'Mar',
    'Apr',
    'May',
    'Jun',
    'Jul',
    'Aug',
    'Sep',
    'Oct',
    'Nov',
    'Dec'
  ]
  const date = new Date(dateString)
  const year = date.getFullYear()
  const month = months[date.getMonth()]
  const day = date.getDate()

  return `${month} ${day}, ${year}`
}

export default formatDate
