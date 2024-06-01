import { images } from '../../constants';

const NotiData = [
    {
      id: '1',
      logo: images.LOGOS.LOGO1,
      notificationTime: 1,
      description: "Your job alert for Backend Developer role in HCM.",
      isJobAlert: true
    },
    {
      id: '2',
      logo: images.LOGOS.LOGO2,
      notificationTime: 2,
      description: "Aman Kumar Accepted your connection request.",
      isConnnectionAccepted: true
    },
    {
      id: '3',
      logo: images.LOGOS.LOGO3,
      notificationTime: 3,
      description: "You have a new job suggestion in the Frontend Devs group. Click to view it.",
      isNewJob: true
    },
    {
      id: '4',
      logo: images.LOGOS.LOGO4,
      notificationTime: 4,
      description: "Kashish was live. How an engineer spends his day in India 😁."
    }
  ]
  
  export default NotiData;