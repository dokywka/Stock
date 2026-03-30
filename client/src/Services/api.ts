import axios from "axios";
import { Login } from "../Types/Login";
import { AuthResponse } from "../Types/AuthResponse";
import { Register } from "../Types/Register";

const api = axios.create({
    baseURL: "https://localhost:7292",
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});

//логин
export const login = async (data: Login) => {
    const response = await api.post<AuthResponse>("/Api/Account/login", data);
    return response.data;
};

export const getPortfolio = async () => {
    const response = await api.get("/Api/Portfolio");
    return response.data;
};

export const register = async (data: Register) => {
    const response = await api.post("/Api/Account/register", data);
    return response.data;
};
export default api;