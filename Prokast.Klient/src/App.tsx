import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Dashboard from './pages/Dashboard';
import RegisterForm from './pages/RegisterForm';
import AddParams from './pages/AddParams';
import DictionaryParams from './pages/DictionaryParams';
import EditParams from './pages/EditParams';
import CreateEmployee from './pages/CreateEmployee';
import PriceList from './pages/PriceList';
import ProductsList from './pages/ProductsList';
import AddProducts from './pages/AddProducts';
import EditProducts from './pages/EditProducts';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/RegisterForm" element={<RegisterForm />} />
        <Route path="/AddParams" element={<AddParams/>} />
        <Route path="/DictionaryParams" element={<DictionaryParams />} />
        <Route path="/EditParams" element={<EditParams />} />
        <Route path="/CreateEmployee" element={<CreateEmployee />} />
        <Route path="/PriceList" element={<PriceList />} />
        <Route path="/ProductsList" element={<ProductsList />} />
        <Route path="/AddProducts" element={<AddProducts />} />
        <Route path="/EditProducts" element={<EditProducts />} />
        <Route path="*" element={<h1>404 Not Found</h1>} />
      </Routes>
    </Router>
  );
}

export default App;
