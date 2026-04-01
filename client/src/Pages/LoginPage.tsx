import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../Services/api";

const LoginPage = () => {
    const [loginValue, setLoginValue] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            const result = await login({
                username: loginValue,
                email: loginValue,
                password: password
            });

            localStorage.setItem("token", result);

            navigate("/portfolio");
        } catch (error) {
            alert("Ошибка входа");
        }
    };

    return (
    <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="bg-white p-8 rounded-lg shadow-md w-full max-w-md">
            <h2 className="text-2xl font-bold mb-6 text-center text-gray-800">Вход</h2>

            <input
                type="text"
                placeholder="Email или Username"
                value={loginValue}
                onChange={(e) => setLoginValue(e.target.value)}
                className="w-full border border-gray-300 rounded px-4 py-2 mb-4 focus:outline-none focus:border-blue-500"
            />

            <input
                type="password"
                placeholder="Пароль"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="w-full border border-gray-300 rounded px-4 py-2 mb-6 focus:outline-none focus:border-blue-500"
            />

            <button
                onClick={handleLogin}
                className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
            >
                Войти
            </button>

            <p className="text-center mt-4 text-gray-500 text-sm">
                Нет аккаунта? <a href="/register" className="text-blue-600 hover:underline">Зарегистрироваться</a>
            </p>
        </div>
    </div>
);
};

export default LoginPage;