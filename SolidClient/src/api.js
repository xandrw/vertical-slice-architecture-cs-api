import axios from "axios";

const api = axios.create({
    baseURL: 'http://localhost:5255/api',
    headers: {
        'Content-Type': 'application/json',
    }
});

api.login = async payload => {
    return await api.post('/login', payload);
};

export default api;