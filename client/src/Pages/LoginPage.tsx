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

            localStorage.setItem("token", result.token);

            navigate("/portfolio");
        } catch (error) {
            alert("Ошибка входа");
        }
    };

    return (
        <div>
            <h2>Вход</h2>

            <input
                type="text"
                placeholder="Email или Username"
                value={loginValue}
                onChange={(e) => setLoginValue(e.target.value)}
            />

            <input
                type="password"
                placeholder="Пароль"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <button onClick={handleLogin}>
                Войти
            </button>
        </div>
    );
};

export default LoginPage;