import React from "react";
import { Link } from "react-router-dom";
import Cookies from "js-cookie";

const Navbar: React.FC = () => {
  const [openDropdown, setOpenDropdown] = React.useState<string | null>(null);

  return (
    <nav
      className="bg-white shadow-md rounded-2xl px-4 py-3 flex flex-wrap justify-between items-center relative"
      onMouseLeave={() => setOpenDropdown(null)}
    >
      {/* Logo */}
      <Link to="/dashboard">
        <h1 className="text-xl font-bold text-gray-800">Prokast</h1>
      </Link>

      {/* Menu */}
      <ul className="flex flex-wrap gap-4 items-center mt-2 md:mt-0">

        {/* Produkty */}
        <li
          className="relative"
          onMouseEnter={() => setOpenDropdown("products")}
        >
          <button className="cursor-pointer text-gray-700 hover:text-blue-500 transition font-medium px-2 py-1 min-w-[100px]">
            Produkty
          </button>
          <div
            className={`absolute left-0 mt-2 bg-white shadow-lg rounded-xl py-2 w-48 z-50 origin-top-left
              transform transition-all duration-150
              ${openDropdown === "products"
                ? "opacity-100 scale-100 translate-y-0 pointer-events-auto"
                : "opacity-0 scale-95 -translate-y-1 pointer-events-none"
              }`}
          >
            <Link
              to="/CreateProduct"
              className="block px-4 py-2 text-gray-700 hover:bg-blue-50 hover:text-blue-600 transition"
            >
              Dodaj Produkt
            </Link>
            <Link
              to="/ProductsList"
              className="block px-4 py-2 text-gray-700 hover:bg-blue-50 hover:text-blue-600 transition"
            >
              Lista Produktów
            </Link>
          </div>
        </li>

        {/* Użytkownicy */}
        <li>
          <Link
            to="/UsersList"
            className="text-gray-700 hover:text-blue-500 transition font-medium"
          >
            Użytkownicy
          </Link>
        </li>

        {/* Magazyny */}
        <li
          className="relative"
          onMouseEnter={() => setOpenDropdown("warehouses")}
        >
          <button className="cursor-pointer text-gray-700 hover:text-blue-500 transition font-medium px-2 py-1 min-w-[100px]">
            Magazyny
          </button>
          <div
            className={`absolute left-0 mt-2 bg-white shadow-lg rounded-xl py-2 w-48 z-50 origin-top-left
              transform transition-all duration-150
              ${openDropdown === "warehouses"
                ? "opacity-100 scale-100 translate-y-0 pointer-events-auto"
                : "opacity-0 scale-95 -translate-y-1 pointer-events-none"
              }`}
          >
            <Link
              to="/AddWarehouse"
              className="block px-4 py-2 text-gray-700 hover:bg-blue-50 hover:text-blue-600 transition"
            >
              Dodaj Magazyn
            </Link>
            <Link
              to="/WarehouseList"
              className="block px-4 py-2 text-gray-700 hover:bg-blue-50 hover:text-blue-600 transition"
            >
              Lista Magazynów
            </Link>
          </div>
        </li>

        {/* Cenniki */}
        <li>
          <Link
            to="/PriceList"
            className="text-gray-700 hover:text-blue-500 transition font-medium"
          >
            Cenniki
          </Link>
        </li>

        {/* Wyloguj */}
        <li>
          <button
            onClick={() => {
              Cookies.remove("token");
              window.location.href = "/";
            }}
            className="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded-xl transition"
          >
            Wyloguj
          </button>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;