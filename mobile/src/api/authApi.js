import { postgreAPI } from "../axios/axios"

const path = '/Auth/users'

export const register = async (name, email, phone, password) => {
    try {
        const registerResponse = await postgreAPI.post(`${path}`, {
            name,
            email,
            phone,
            password,
        })

        return registerResponse
    }
    catch (err) {
        console.log(err)
        throw err
    }
}

export const login = async (email, password) => {
    try {
        const loginResponse = await postgreAPI.post(`${path}/login`, {
            email,
            password
        })

        return loginResponse
    }
    catch (err) {
        console.log(err)
        throw err
    }
}

export const refreshToken = async (userId) => {
    try {
        const rtResponse  = await postgreAPI.post(`${path}/${userId}/refresh-tokens`,{
            userId
        })
        
        return rtResponse
    } catch (error) {
        console.log(err)
        throw err
    }
}