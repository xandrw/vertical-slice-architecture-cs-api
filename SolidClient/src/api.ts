import axios, {AxiosInstance} from "axios";
import {setAuth} from "./Store/auth";

interface CustomApiInstance extends AxiosInstance {
    login: (credentials: { email: string; password: string }) => Promise<any>;
    listUsers: (params: { pageNumber?: number; pageSize?: number }) => Promise<any>;
}

const api = axios.create({
    baseURL: 'http://localhost:5255/api',
    headers: {'Content-Type': 'application/json'}
}) as CustomApiInstance;

api.login = async ({email, password} : { email: string, password: string }) => {
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