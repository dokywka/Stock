import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./Pages/LoginPage";
import PortfolioPage from "./Pages/PortfolioPage";
import StocksPage from "./Pages/StocksPage";
import RegisterPage from "./Pages/RegisterPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/portfolio" element={<PortfolioPage />} />
        <Route path="/stocks" element={<StocksPage />} />
        <Route path="/register" element={<RegisterPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;