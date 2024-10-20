import axios from "axios";
import {setAuth} from "./Store/auth";

const api = axios.create({
    baseURL: 'http://localhost:5255/api',
    headers: {'Content-Type': 'application/json'}
});

api.login = async ({email, password}) => {
    const response = await api.post('/login', {email, password});

    if (response.data.token) {
        api.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
    }
    
    setAuth(response.data);
    localStorage.setItem('token', response.data.token);
    localStorage.setItem('user', JSON.stringify(response.data.user));

    return response;
}

api.listUsers = async ({pageNumber = 1, pageSize = 10}) =>
    await api.get(`/admin/users?pageNumber=${pageNumber}&pageSize=${pageSize}`);

export default api;