import LoginRepository from './LoginRepository'
import ImageRepository from './ImageRepository'

const repositories = {
    login: LoginRepository,
    image: ImageRepository
}

export const RepositoryFactory = {
    get: name => repositories[name]
}