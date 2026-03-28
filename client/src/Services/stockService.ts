import api from "./api";
import { Stock } from "../Types/Stock";

export const getStocks=async():Promise<Stock[]> => {
    const response=await api.get('/Api/Stock');

    return response.data;
};