import React from "react";
import { BrowserRouter ,Routes,Route} from "react-router-dom";
import StocksPage from "./Pages/StocksPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<StocksPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
