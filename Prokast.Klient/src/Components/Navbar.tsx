import React from 'react';
import { Link } from 'react-router-dom';

const Navbar: React.FC = () => {
  return (
    <nav className="bg-white shadow-md rounded-2xl px-6 py-3 flex justify-between items-center">
      <Link to="/dashboard">
        <h1 className="text-xl font-bold text-gray-800">Prokast</h1>
      </Link>
      <ul className="flex space-x-4">
        <li>
          <Link
            to="/RegisterForm"
            className="text-gray-700 hover:text-blue-500 transition font-medium"
          >
            Rejestracja
          </Link>
        </li>
        <li>
          <Link
            to="/AddParams"
            className="text-gray-700 hover:text-blue-500 transition font-medium"
          >
            Dodaj Parametry
          </Link>
        </li>
        <li>
          <Link
            to="/addProducts"
            className="text-gray-700 hover:text-blue-500 transition font-medium"
          >
            Dodaj Produkt
          </Link>
          
        </li>
        <li>
          <Link
            to="/logout"
            className="bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded-xl transition"
          >
            Wyloguj
          </Link>
        </li>
        
      </ul>
    </nav>
  );
};

export default Navbar;
