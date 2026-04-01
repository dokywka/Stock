export interface Portfolio {
    portfolioId: number;
    stockId: number;
    quantity: number;
    purchasePrice: number;
    stock: {
        symbol: string;
        companyName: string;
        purchase: number;
    }
}