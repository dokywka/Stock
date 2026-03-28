import React,{useEffect, useState} from "react";
import { Stock } from "../Types/Stock";
import { getStocks } from "../Services/stockService";

const StocksPage=()=>{
    const [stocks,setStocks]=useState<Stock[]>([]);//List<Stock> stocks = new List<Stock>();
    useEffect(()=>{//запускается один раз, если бы был [ticker], запустилось бы каждый раз когда меняем тикер
        getStocks().then(data=>setStocks(data));
    },[]);
    
    return(
        <div className="p-4 bg-blue-500">
            {stocks.map(stock=>(
                <div key={stock.id}>
                    <h2>{stock.symbol}</h2>
                    <p>{stock.companyName}</p>
                    <p>{stock.purchase}</p>
                </div>
            ))}
        </div>
    );
};

export default StocksPage;