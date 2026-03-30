import { useEffect, useState } from "react";
import { getPortfolio } from "../Services/api";

const PortfolioPage = () => {
    const [stocks, setStocks] = useState<any[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const data = await getPortfolio();
                setStocks(data);
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
        <div>
            <h2>Портфолио</h2>

            {stocks.length === 0 ? (
                <p>У вас нет акций</p>
            ) : (
                stocks.map((stock, index) => (
                    <div key={index}>
                        <p>Акция: {stock.stockSymbol}</p>
                        <p>Количество: {stock.quantity}</p>
                        <p>Цена: {stock.price}</p>
                        <hr />
                    </div>
                ))
            )}
        </div>
    );
};

export default PortfolioPage;