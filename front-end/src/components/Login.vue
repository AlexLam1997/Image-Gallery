<template>
    <div id = "formblock">
        <div id="login">
            <h1>Login</h1>
            <input type="text" name="username" v-model="loginInput.username" placeholder="Username" />
            <input type="password" name="password" v-model="loginInput.password" placeholder="Password" />
            <button type="button" v-on:click="login()">Login</button>
        </div>
        <div id="sign-up">
            <h1>Sign up</h1>
            <input type="text" name="username" v-model="signUpInput.username" placeholder="Username" />
            <input type="password" name="password" v-model="signUpInput.password" placeholder="Password" />
            <button type="button" v-on:click="signUp()">Sign Up</button>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import {baseUrl} from './variables.js'
    import {RepositoryFactory} from '../Repositories/RepositoryFactory'

    const LoginRepository = RepositoryFactory.get('login')

    export default {
        name: 'Login',
        data() {
            return {
                loginInput: {
                    username: "",
                    password: ""
                },
                signUpInput: {
                    username: "",
                    password: ""
                }
            }
        },
        
        methods: {
            login() {
                if(this.loginInput.username != "" && this.loginInput.password != "") {
                    LoginRepository.login(this.loginInput.username, this.loginInput.password).then( (resp) => {
                        this.$store.dispatch('updateKey', resp.data.payload.accessToken)
                        this.$router.push('/home')
                    })
                    this.loginInput.username = ""
                    this.loginInput.password = ""
                } else {
                    console.log("A username and password must be present");
                }
            },
            signUp() {
                if(this.signUpInput.username != "" && this.signUpInput.password != "") {
                    let request = {
                        Username: this.signUpInput.username,
                        Password: this.signUpInput.password
                    }
                    axios.put(baseUrl + "api/Users", request).then(function(response){
                        //TODO failure
                        console.log(response)
                    })
                    this.signUpInput.username = ""
                    this.signUpInput.password = ""
                } else {
                    console.log("A username and password must be present");
                }
            },
            updateKey(key){
                this.$store.dispatch('updateKey', key)
            }
        }
    }
</script>

<style scoped>
    #login, #sign-up {
        width: 500px;
        border: 1px solid #CCCCCC;
        background-color: #FFFFFF;
        margin: auto;
        margin-top: 100px;
        padding: 20px;
    }
</style>