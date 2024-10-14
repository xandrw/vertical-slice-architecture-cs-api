import {AxiosError} from 'axios';
import {object, string} from 'yup';
import {createSignal} from "solid-js";
import Input from "../Components/Input";
import PrimaryButton from "../Components/PrimaryButton";
import Alert from "../Components/Alert";
import api from "../api";
import {setAuth} from "../Store/auth";
import {useNavigate} from "@solidjs/router";

const Login = () => {
    const navigate = useNavigate();
    const [email, setEmail] = createSignal('');
    const [password, setPassword] = createSignal('');

    const emptyErrors = {email: '', password: '', general: ''};
    const [errors, setErrors] = createSignal(emptyErrors);

    const handleLogin = async e => {
        e.preventDefault();
        e.stopPropagation();
        await validateInput();

        if (errors().email || errors().password) {
            return;
        }

        try {
            const response = await api.login({email: email(), password: password()});

            setAuth(response.data);
            navigate('/admin/users');
        } catch (error) {
            if (error instanceof AxiosError) {
                setErrors({...errors(), general: error.response.data.error});
                console.error('Login error:', error);
                return;
            }

            setErrors({...errors(), general: error.message});
            console.error('Login error:', error);
        }
    };

    const validateInput = async () => {
        const loginSchema = object({
            email: string().email().required().min(6).max(60),
            password: string().required().min(8).max(60)
        });

        try {
            await loginSchema.validate({email: email(), password: password()}, {abortEarly: false});
            setErrors(emptyErrors);
        } catch (error) {
            const newErrors = {...emptyErrors};

            error.inner.forEach(validationError => newErrors[validationError.path] = validationError.message);
            setErrors(newErrors);
        }
    };

    return (
        <div class="flex items-center min-h-screen p-6 bg-gray-50 text-gray-700">
            <div class="flex-1 h-full max-w-sm mx-auto overflow-hidden bg-white rounded-lg shadow-xl">
                <div class="flex items-center justify-center p-6">
                    <form name="login" on:submit={handleLogin} class="w-full">
                        <h1 class="mb-4 text-xl font-semibold">Login</h1>
                        <Input name="email"
                               type="text"
                               valueSignal={email}
                               valueSetter={setEmail}
                               errorsSignal={errors}/>
                        <Input name="password"
                               type="password"
                               valueSignal={password}
                               valueSetter={setPassword}
                               errorsSignal={errors}/>

                        <Alert errorKey="general" errorsSignal={errors}/>
                        <PrimaryButton type="submit" fullWidth>Login</PrimaryButton>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default Login;