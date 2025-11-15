import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
import Navbar from '../Components/Navbar';

interface Product {
  id: number;
  name: string;
  sku: string;
  photo: string;
  additionDate: string;
}

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<string>('Wszystko');
  const [selectedPriceRange, setSelectedPriceRange] = useState<string>('Wszystko');
  

  const fetchProducts = async () => {
    try {
      setLoading(true);
      const token = Cookies.get("token");

      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        setLoading(false);
        return;
      }

      const nubmerOfIitemsOnListString = prompt("Podaj ilość produktów do wyświetlenia na liście:", "10");
      const nubmerOfIitemsOnList = nubmerOfIitemsOnListString ? parseInt(nubmerOfIitemsOnListString) : null;

      

      const response = await axios.post(
        "https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/products/productsListFiltered",
        {
          name: "",
          sku: ""
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            Accept: "application/json"
          }
        ,
          params:{
            pageNumber: 1,
            itemsNumber: nubmerOfIitemsOnList ?? 10

          }
        
        }
        
      );

      const model = response.data?.model ?? response.data;

      if (!model || model.length === 0) {
        setError("Brak produktów dla podanego magazynu.");
        return;
      }

      setProducts(
        model.map((item: any) => ({
          id: item.id,
          name: item.name,
          sku: item.sku,
          photo: item.photo,
          additionDate: item.additionDate
        }))
      );

      setError("");
    } catch (err: any) {
      console.error("Błąd API:", err.response?.data ?? err);
      setError("Nie udało się pobrać listy produktów.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  const filteredProducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center text-gray-700 text-lg">
        Wczytywanie produktów...
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen flex flex-col items-center justify-center text-center text-red-600">
        <p>{error}</p>
        <button
          onClick={fetchProducts}
          className="mt-4 px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700"
        >
          Spróbuj ponownie
        </button>
      </div>
    );
  }

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
          <h1 className="text-2xl font-bold text-gray-800 mb-6">Lista produktów</h1>

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
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj
                    </button>

                    <button
                      className="px-4 py-2 bg-red-600 text-white rounded-xl hover:bg-red-700 transition"
                    >
                      Usuń
                    </button>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-gray-600 text-center mt-10">Brak produktów do wyświetlenia.</p>
          )}
        </main>
      </div>
    </div>
  );
};

export default ProductList;
