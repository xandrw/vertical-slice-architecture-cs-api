import {createStore} from "solid-js/store"

export const [auth, setAuth] = createStore({
    token: localStorage.getItem('token') ?? '',
    user: JSON.parse(<string>localStorage.getItem('user')) ?? {id: 0, email: '', role: ''},
    get isAdmin() {
        return this.user.role === 'Admin';
    }
});