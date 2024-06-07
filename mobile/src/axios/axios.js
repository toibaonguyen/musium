import axios from 'axios'

const POSTGRE_BASE_URL = 'http://10.0.2.2:5227/api'

export const postgreAPI = axios.create({
    baseURL: POSTGRE_BASE_URL
})