import { postgreAPI } from "../axios/axios"

const path = '/CloudMessageRegistrationToken'

export const register = async (accessToken) => {
    try {
        const registerResponse = await postgreAPI.put(`${path}`, {
            token: accessToken
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