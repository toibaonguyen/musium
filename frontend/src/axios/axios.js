import axios from 'axios'

const POSTGRE_BASE_URL = 'http://192.168.1.31:5227/api'

export const postgreAPI = axios.create({
    baseURL: POSTGRE_BASE_URL
})