import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
import Navbar from '../Components/Navbar';
import { useNavigate } from 'react-router-dom';
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";

const API_URL = process.env.REACT_APP_API_URL;

interface Product {
  id: number;
  name: string;
  sku: string;
  photo: string;
  additionDate: string;
  ean: string;
  description?: string;
}

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const [productToDelete, setProductToDelete] = useState<Product | null>(null);
  const navigate = useNavigate();

  const fetchProducts = async () => {
    try {
      setLoading(true);
      const token = Cookies.get("token");

      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        setLoading(false);
        return;
      }

      const response = await axios.post(
        `${API_URL}/api/products/productsListFiltered`,
        { name: "", sku: "" },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            Accept: "application/json"
          },
          params: {
            pageNumber: currentPage,
            itemsNumber: itemsPerPage
          }
        }
      );

      const model = response.data?.model ?? response.data;
      const totalItems = response.data?.totalItems ?? 0;

      setTotalPages(Math.ceil(totalItems / itemsPerPage));

      if (!model || model.length === 0) {
        setError("Brak produktów dla podanego magazynu.");
        setProducts([]);
        return;
      }

      setProducts(
        model.map((item: any) => ({
          id: item.id,
          name: item.name,
          sku: item.sku,
          photo: item.photo,
          additionDate: item.additionDate,
          ean: item.ean,
          description: item.description,
        }))
      );

      setError("");
    } catch (err: any) {
      setError("Nie udało się pobrać listy produktów.");
    } finally {
      setLoading(false);
    }
  };

  const openDeleteDialog = (product: Product) => {
    setProductToDelete(product);
    setIsDeleteOpen(true);
  };

  const confirmDelete = async () => {
    if (!productToDelete) return;

    try {
      const token = Cookies.get("token");

      if (!token) {
        console.error("Brak tokenu autoryzacyjnego.");
        return;
      }

      await axios.delete(`${API_URL}/api/products/${productToDelete.id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });

      setIsDeleteOpen(false);
      setProductToDelete(null);
      fetchProducts();
    } catch (err) {
      console.error("Błąd podczas usuwania produktu:", err);
      alert("Nie udało się usunąć produktu.");
    }
  };

  useEffect(() => {
    fetchProducts();
  }, [currentPage, itemsPerPage]);

  const filteredProducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const getPageNumbers = (
    current: number,
    total: number,
    visibleAround = 2
  ) => {
    const pages: (number | string)[] = [];
    const start = Math.max(2, current - visibleAround);
    const end = Math.min(total - 1, current + visibleAround);

    pages.push(1);

    if (start > 2) pages.push('...');

    for (let i = start; i <= end; i++) pages.push(i);

    if (end < total - 1) pages.push('...');

    if (total > 1) pages.push(total);

    return pages;
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200">
      <Navbar />

      <div className="max-w-7xl mx-auto flex flex-col lg:flex-row mt-8 p-4 gap-6">

        <aside className="w-full lg:w-1/4 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 h-fit">
          <h2 className="text-xl font-bold text-gray-800 mb-4">Filtry</h2>

          <div className="mb-6">
            <label className="block text-gray-600 mb-2 font-semibold">Szukaj produktu</label>
            <input
              type="text"
              placeholder="np. Imbryczek"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm focus:ring focus:ring-blue-300"
            />
          </div>

          <div className="mb-6">
            <label className="block text-gray-600 mb-2 font-semibold">Ilość produktów na stronę</label>
            <select
              value={itemsPerPage}
              onChange={(e) => {
                setItemsPerPage(Number(e.target.value));
                setCurrentPage(1);
              }}
              className="w-full p-3 border rounded-xl shadow-sm focus:ring focus:ring-blue-300"
            >
              <option value={1}>1</option>
              <option value={2}>2</option>
              <option value={5}>5</option>
              <option value={10}>10</option>
              <option value={25}>25</option>
            </select>
          </div>

          <div className="mt-6">
            <button
              onClick={fetchProducts}
              className="w-full bg-blue-600 text-white font-semibold py-3 rounded-xl hover:bg-blue-700 transition"
            >
              Zastosuj filtry
            </button>
          </div>
        </aside>

        <main className="flex-1">
          <div className="flex items-center justify-between mb-6">
            <h1 className="text-2xl font-bold text-gray-800">Lista produktów</h1>

            <button
              onClick={() => navigate("/CreateProduct")}
              className="flex items-center gap-2 bg-green-600 text-white px-4 py-2 rounded-xl hover:bg-green-700 transition"
            >
              <FaPlus /> Dodaj produkt
            </button>
          </div>

          {filteredProducts.length > 0 ? (
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {filteredProducts.map((product) => (
                <div
                  key={product.id}
                  className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 hover:shadow-xl transition"
                >
                  <img
                    src={product.photo}
                    alt={product.name}
                    className="w-full h-48 object-contain rounded-xl mb-4"
                  />
                  <h2 className="text-2xl font-bold text-gray-800 mb-2">{product.name}</h2>
                  <div className="text-gray-600 mb-4">
                    <p>SKU: {product.sku}</p>
                    <p>Dodano: {new Date(product.additionDate).toLocaleString()}</p>
                  </div>
                  <div className="flex gap-4 mt-6">
                    <button
                      onClick={() => openDeleteDialog(product)}
                      className="px-4 py-2 bg-red-600 text-white rounded-xl hover:bg-red-700 transition"
                    >
                      Usuń
                    </button>
                    <button
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                      onClick={() => navigate(`/EditProducts/${product.id}`)}
                    >
                      Edytuj
                    </button>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-gray-600 text-center mt-10">Brak produktów do wyświetlenia.</p>
          )}

          <div className="flex justify-center mt-10 gap-2">
            <button
              disabled={currentPage === 1}
              onClick={() => setCurrentPage(currentPage - 1)}
              className="px-3 py-2 bg-gray-300 rounded disabled:opacity-50"
            >
              ←
            </button>

            {getPageNumbers(currentPage, totalPages).map((page, idx) =>
              typeof page === 'number' ? (
                <button
                  key={idx}
                  onClick={() => setCurrentPage(page)}
                  className={`px-3 py-2 rounded ${
                    currentPage === page ? "bg-blue-600 text-white" : "bg-gray-200"
                  }`}
                >
                  {page}
                </button>
              ) : (
                <span key={idx} className="px-3 py-2">{page}</span>
              )
            )}

            <button
              disabled={currentPage === totalPages}
              onClick={() => setCurrentPage(currentPage + 1)}
              className="px-3 py-2 bg-gray-300 rounded disabled:opacity-50"
            >
              →
            </button>
          </div>
        </main>

        {isDeleteOpen && productToDelete && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
              <h2 className="text-xl font-bold text-gray-800 mb-2">Potwierdzenie usunięcia</h2>
              <p>Czy na pewno chcesz usunąć produkt <strong>{productToDelete.name}</strong>?</p>
              <div className="flex justify-end gap-3 pt-2">
                <button
                  type="button"
                  onClick={confirmDelete}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Tak
                </button>
                <button
                  type="button"
                  onClick={() => setIsDeleteOpen(false)}
                  className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
                >
                  Nie
                </button>
              </div>
            </div>
          </div>
        )}

      </div>
    </div>
  );
};

export default ProductList;