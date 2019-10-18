 import Repository from './Repositories'
export default {
    upload(files){
        return Repository.post("images/upload", files)
    }
}