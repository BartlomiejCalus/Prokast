import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
import Navbar from '../Components/Navbar';

interface Product {
  name: string;
  sku: string;
  ean: string;
  description: string;
  additionalDescriptions?: { title: string; value: string; regionID: number }[];
  additionalNames?: { title: string; value: string; regionID: number }[];
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
      const token = Cookies.get('token');

      if (!token) {
        setError('Brak tokenu autoryzacyjnego.');
        setLoading(false);
        return;
      }

      const productID = 1;
      const clientID = 1;

      const response = await axios.get(
        `https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/products/products/${productID}?clientID=${clientID}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            Accept: 'application/json',
          },
        }
      );

      const model = response.data?.model ?? response.data;
      if (!model) {
        setError('Brak danych produktu w odpowiedzi API.');
        return;
      }

      setProducts([model]);
      setError('');
    } catch (err: any) {
      console.error('Błąd podczas pobierania produktu:', err);
      console.error('Odpowiedź serwera:', err.response?.data);
      setError('Nie udało się pobrać produktu.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  const handleAddToCart = (product: Product) => {
    alert(`Dodano produkt "${product.name}" do koszyka.`);
  };

  const handleViewDetails = (product: Product) => {
    alert(`Otwieranie szczegółów produktu: ${product.name}`);
  };

  const filteredProducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center text-gray-700 text-lg">
        Wczytywanie produktu...
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
        {/* Pasek filtrów po lewej stronie */}
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
            <label className="block text-gray-600 mb-2 font-semibold">Kategoria</label>
            <select
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm"
            >
              <option>Wszystko</option>
              <option>AGD</option>
              <option>Elektronika</option>
              <option>Dom i ogród</option>
              <option>Inne</option>
            </select>
          </div>

          <div>
            <label className="block text-gray-600 mb-2 font-semibold">Zakres cen</label>
            <select
              value={selectedPriceRange}
              onChange={(e) => setSelectedPriceRange(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm"
            >
              <option>Wszystko</option>
              <option>0 - 50 zł</option>
              <option>50 - 200 zł</option>
              <option>200 - 500 zł</option>
              <option>500+ zł</option>
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

        {/* Lista produktów po prawej stronie */}
        <main className="flex-1">
          <h1 className="text-2xl font-bold text-gray-800 mb-6">Szczegóły produktu</h1>

          {filteredProducts.length > 0 ? (
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {filteredProducts.map((product, index) => (
                <div
                  key={index}
                  className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 hover:shadow-xl transition"
                >
                  <h2 className="text-2xl font-bold text-gray-800 mb-4">{product.name}</h2>
                  <p className="text-gray-600 mb-4">{product.description}</p>

                  {product.additionalDescriptions && product.additionalDescriptions.length > 0 && (
                    <div className="mb-4">
                      <h3 className="font-semibold">Dodatkowe opisy:</h3>
                      <ul className="list-disc pl-6 text-gray-700">
                        {product.additionalDescriptions.map((desc, i) => (
                          <li key={i}>
                            <strong>{desc.title}</strong>: {desc.value}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  {product.additionalNames && product.additionalNames.length > 0 && (
                    <div className="mb-4">
                      <h3 className="font-semibold">Dodatkowe nazwy:</h3>
                      <ul className="list-disc pl-6 text-gray-700">
                        {product.additionalNames.map((n, i) => (
                          <li key={i}>
                            <strong>{n.title}</strong>: {n.value}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  <div className="text-sm text-gray-500 mt-4">
                    <p><strong>SKU:</strong> {product.sku}</p>
                    <p><strong>EAN:</strong> {product.ean}</p>
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
