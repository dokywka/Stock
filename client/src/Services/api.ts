import axios from "axios";
import { Login } from "../Types/Login";
import { AuthResponse } from "../Types/AuthResponse";
import { Register } from "../Types/Register";
import { Portfolio } from "../Types/Portfolio";

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
    const response = await api.get("/Api/Portfolio/stocks");
    return response.data;
};

export const register = async (data: Register) => {
    const response = await api.post("/Api/Account/register", data);
    return response.data.data;
};
export const getPortfolioValue=async ()=>{
    const response=await api.get("/Api/Portfolio/value");
    return response.data.data;
};
export default api;