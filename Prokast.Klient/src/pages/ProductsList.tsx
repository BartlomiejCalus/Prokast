import React, { useState } from 'react';
import Navbar from '../Components/Navbar';

const ProductList: React.FC = () => {
  const [selectedCategory, setSelectedCategory] = useState('');
  const [priceMin, setPriceMin] = useState('');
  const [priceMax, setPriceMax] = useState('');
  const [condition, setCondition] = useState('');
  const [availability, setAvailability] = useState('');
  const [sortOrder, setSortOrder] = useState('');
  const [searchTerm, setSearchTerm] = useState('');

  const products = [
    {
      title: 'Czerwona kurtka zimowa',
      description: 'Stylowa i ciepła kurtka idealna na zimowe dni.',
      price: '249.99 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Kurtka',
      location: 'Warszawa',
      date: '21 lipca 2025',
    },
    {
      title: 'Smartfon Galaxy X10',
      description: 'Nowoczesny smartfon z dużym ekranem i świetnym aparatem.',
      price: '1599.00 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Smartfon',
      location: 'Kraków',
      date: '20 lipca 2025',
    },
    {
      title: 'Fotel gamingowy',
      description: 'Wygodny fotel do grania z regulowanym oparciem i podłokietnikami.',
      price: '549.99 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Fotel',
      location: 'Poznań',
      date: '19 lipca 2025',
    },
  ];

  const filteredProducts = products.filter((product) =>
    product.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleEdit = (title: string) => {
    alert(`Edytuj produkt: ${title}`);
  };

  const handleDelete = (title: string) => {
    alert(`Usuń produkt: ${title}`);
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />

      <div className="max-w-6xl mx-auto mt-8">
        <h1 className="text-2xl font-bold text-center text-gray-800 mb-6">Lista produktów</h1>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Sidebar z filtrami */}
          <div className="lg:col-span-1 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6">
            <h2 className="text-xl font-bold text-gray-800 mb-4">Filtry</h2>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="category">Kategoria:</label>
            <select
              id="category"
              className="w-full p-2 mb-4 border rounded-xl"
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="odziez">Odzież</option>
              <option value="elektronika">Elektronika</option>
              <option value="dom">Dom i ogród</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700">Cena (zł):</label>
            <div className="flex gap-2 mb-4">
              <input
                type="number"
                placeholder="Od"
                className="w-1/2 p-2 border rounded-xl"
                value={priceMin}
                onChange={(e) => setPriceMin(e.target.value)}
              />
              <input
                type="number"
                placeholder="Do"
                className="w-1/2 p-2 border rounded-xl"
                value={priceMax}
                onChange={(e) => setPriceMax(e.target.value)}
              />
            </div>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="condition">Stan produktu:</label>
            <select
              id="condition"
              className="w-full p-2 mb-4 border rounded-xl"
              value={condition}
              onChange={(e) => setCondition(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="nowy">Nowy</option>
              <option value="uzywany">Używany</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="availability">Dostępność:</label>
            <select
              id="availability"
              className="w-full p-2 mb-4 border rounded-xl"
              value={availability}
              onChange={(e) => setAvailability(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="dostepny">W magazynie</option>
              <option value="brak">Brak w magazynie</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="sort">Sortuj według:</label>
            <select
              id="sort"
              className="w-full p-2 border rounded-xl"
              value={sortOrder}
              onChange={(e) => setSortOrder(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="cena-rosnaco">Cena: od najniższej</option>
              <option value="cena-malejaco">Cena: od najwyższej</option>
              <option value="najnowsze">Najnowsze</option>
            </select>
          </div>

          {/* Lista produktów z wyszukiwarką */}
          <div className="lg:col-span-2 flex flex-col gap-6">
            <input
              type="text"
              placeholder="Szukaj po nazwie produktu..."
              className="w-full p-3 border rounded-xl"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />

            {filteredProducts.map((product, index) => (
              <div
                key={index}
                className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-4 flex flex-col sm:flex-row gap-4 items-start"
              >
                <img
                  src={product.image}
                  alt={product.title}
                  className="w-full sm:w-60 h-auto rounded-xl object-cover"
                />
                <div className="flex-1">
                  <h2 className="text-xl font-semibold text-gray-800">{product.title}</h2>
                  <p className="text-gray-600 mt-2">{product.description}</p>
                  <p className="text-blue-700 font-bold text-lg mt-4">{product.price}</p>
                  <div className="text-sm text-gray-500 mt-2">
                    <p>{product.location}</p>
                    <p>{product.date}</p>
                  </div>

                  {/* Przyciski Edytuj / Usuń */}
                  <div className="mt-4 flex gap-3">
                    <button
                      onClick={() => handleEdit(product.title)}
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj
                    </button>
                    <button
                      onClick={() => handleDelete(product.title)}
                      className="px-4 py-2 bg-red-500 text-white rounded-xl hover:bg-red-600 transition"
                    >
                      Usuń
                    </button>
                  </div>
                </div>
              </div>
            ))}

            {filteredProducts.length === 0 && (
              <p className="text-gray-600 text-center">Brak wyników pasujących do wyszukiwania.</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductList;
