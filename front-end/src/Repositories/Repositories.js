import axios from 'axios'
import store from '../store/store'

const baseDomain = "https://localhost:44394"
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL,
    headers : {"Authorization" : `Bearer ${store.getters.accessKey}`}
})
