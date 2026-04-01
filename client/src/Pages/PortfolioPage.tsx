import { useEffect, useState } from "react";
import { getPortfolio, getPortfolioValue } from "../Services/api";
import { Portfolio } from "../Types/Portfolio";

const PortfolioPage = () => {
    const [stocks, setStocks] = useState<Portfolio[]>([]);
    const [loading, setLoading] = useState(true);
    const [portfValue,setPortfolioValue]=useState<number>();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await getPortfolio();
                const value =await getPortfolioValue();
                setStocks(data);
                setPortfolioValue(value);
            } catch (error) {
                console.log("Ошибка загрузки портфолио", error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    if (loading) return <div>Загрузка...</div>;

    return (
    <div className="p-6 bg-gray-50 min-h-screen">
        <h2 className="text-2xl font-bold mb-4">Портфолио</h2>
        <p className="text-lg mb-6 text-gray-600">Стоимость портфеля: <span className="font-bold text-green-600">${portfValue}</span></p>
        
        {stocks.length === 0 ? <p className="text-gray-500">Нет акций</p> :
        <table className="w-full border-collapse bg-white shadow rounded-lg">
            <thead className="bg-gray-200 text-gray-700">
                <tr>
                    <th className="px-4 py-3 text-left">Тикер</th>
                    <th className="px-4 py-3 text-left">Компания</th>
                    <th className="px-4 py-3 text-left">Количество</th>
                    <th className="px-4 py-3 text-left">Цена покупки</th>
                    <th className="px-4 py-3 text-left">Текущая цена</th>
                </tr>
            </thead>
            <tbody>
                {stocks.map((stock, index) => (
                    <tr key={index} className="border-t hover:bg-gray-50">
                        <td className="px-4 py-3 font-bold">{stock.stock.symbol}</td>
                        <td className="px-4 py-3">{stock.stock.companyName}</td>
                        <td className="px-4 py-3">{stock.quantity}</td>
                        <td className="px-4 py-3">${stock.purchasePrice}</td>
                        <td className="px-4 py-3 text-green-600">${stock.stock.purchase}</td>
                    </tr>
                ))}
            </tbody>
        </table>}
    </div>
);
};

export default PortfolioPage;