import Repository from './Repositories'

export default {
    login(username, password){
        let request = {
            Username: username,
            Password: password
        }
        return Repository.post("authentication", request)
    }
}