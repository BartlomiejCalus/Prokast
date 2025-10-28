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
import ProtectedRoute from './Components/ProtectedRoute';

function App() {
  return (
    <Router>
      <Routes>
        {/* Publiczna ścieżka (logowanie / rejestracja) */}
        <Route path="/" element={<Home />} />
        <Route path="/RegisterForm" element={<RegisterForm />} />

        {/* Chronione ścieżki */}
        <Route
          path="/dashboard"
          element={
            <ProtectedRoute>
              <Dashboard />
            </ProtectedRoute>
          }
        />
        <Route
          path="/AddParams"
          element={
            <ProtectedRoute>
              <AddParams />
            </ProtectedRoute>
          }
        />
        <Route
          path="/DictionaryParams"
          element={
            <ProtectedRoute>
              <DictionaryParams />
            </ProtectedRoute>
          }
        />
        <Route
          path="/EditParams"
          element={
            <ProtectedRoute>
              <EditParams />
            </ProtectedRoute>
          }
        />
        <Route
          path="/CreateEmployee"
          element={
            <ProtectedRoute>
              <CreateEmployee />
            </ProtectedRoute>
          }
        />
        <Route
          path="/PriceList"
          element={
            <ProtectedRoute>
              <PriceList />
            </ProtectedRoute>
          }
        />
        <Route
          path="/ProductsList"
          element={
            <ProtectedRoute>
              <ProductsList />
            </ProtectedRoute>
          }
        />
        <Route
          path="/AddProducts"
          element={
            <ProtectedRoute>
              <AddProducts />
            </ProtectedRoute>
          }
        />
        <Route
          path="/EditProducts"
          element={
            <ProtectedRoute>
              <EditProducts />
            </ProtectedRoute>
          }
        />

        {/* 404 */}
        <Route path="*" element={<h1>404 Not Found</h1>} />
      </Routes>
    </Router>
  );
}

export default App;
