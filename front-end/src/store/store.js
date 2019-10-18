import Vuex from 'vuex'
import Vue from 'vue'
import * as Cookies from 'js-cookie'
import createPersistedState from 'vuex-persistedstate'


Vue.use(Vuex)

const store = new Vuex.Store({
    state:{
        accessKey : ""
    },
    getters: {
        accessKey: state => {
            return state.accessKey
        }
    },
    mutations:{
        updateKey(state, key){
            state.accessKey = key
        }
    },
    actions:{
        updateKey(context, key){
            context.state.accessKey = key
        }
    },
    plugins: [
        createPersistedState({
          getState: (key) => Cookies.getJSON(key),
          setState: (key, state) => Cookies.set(key, state, { expires: 3, secure: true })
        })
      ]
});

export default store