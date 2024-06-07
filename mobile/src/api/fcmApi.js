import { postgreAPI } from "../axios/axios"

const path = '/CloudMessageRegistrationToken'

export const register = async (accessToken, fcmToken) => {
    try {
        const registerResponse = await postgreAPI.put(`${path}`, {
            token: fcmToken
        },
        {
            headers: {
                'Authorization': `Bearer ${accessToken}`, // nếu cần token
            }
        })

        return registerResponse
    }
    catch (err) {
        console.log(err)
        throw err
    }
}