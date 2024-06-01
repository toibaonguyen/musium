import axios from 'axios'

const POSTGRE_BASE_URL = 'http://192.168.1.23:5227/api/v1'

export const postgreAPI = axios.create({
    baseURL: POSTGRE_BASE_URL
})