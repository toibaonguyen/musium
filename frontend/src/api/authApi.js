import { postgreAPI } from "../axios/axios"

const path = '/Auth/users/'

export const register = async (name, email, phone, password, username) => {
    try {
        const registerResponse = await postgreAPI.post(`${path}`, {
            name,
            email,
            phone,
            password,
            username
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
        console.log(err.response.data)
        throw err
    }
}