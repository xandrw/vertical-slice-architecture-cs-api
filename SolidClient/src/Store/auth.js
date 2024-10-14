import {createStore} from "solid-js/store"

export const [auth, setAuth] = createStore({
    token: '',
    user: {
        id: 0,
        email: '',
        role: ''
    }
});