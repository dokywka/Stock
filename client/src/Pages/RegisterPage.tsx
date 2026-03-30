import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../Services/api";

const RegisterPage = () => {
    const [data, setData] = useState({
        username: "",
        email: "",
        password: ""
    });

    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setData({
            ...data,
            [e.target.name]: e.target.value
        });
    };

    const handleRegister = async () => {
        try {
            await register(data);

            alert("Регистрация успешна");
            navigate("/"); // на логин
        } catch (error) {
            console.log(error);
            alert("Ошибка регистрации");
        }
    };

    return (
        <div>
            <h2>Регистрация</h2>

            <input
                type="text"
                name="username"
                placeholder="Username"
                value={data.username}
                onChange={handleChange}
            />

            <input
                type="email"
                name="email"
                placeholder="Email"
                value={data.email}
                onChange={handleChange}
            />

            <input
                type="password"
                name="password"
                placeholder="Password"
                value={data.password}
                onChange={handleChange}
            />

            <button onClick={handleRegister}>
                Зарегистрироваться
            </button>
        </div>
    );
};

export default RegisterPage;