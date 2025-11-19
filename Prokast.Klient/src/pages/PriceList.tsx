import React, { useState } from 'react';
import Navbar from '../Components/Navbar';

const API_URL = process.env.REACT_APP_API_URL; 

const PriceList: React.FC = () => {
  const [modalOpen, setModalOpen] = useState(false);
  const [priceListName, setPriceListName] = useState('');
  const [priceLists, setPriceLists] = useState<string[]>([]);

  const openModal = () => setModalOpen(true);
  const closeModal = () => setModalOpen(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!priceListName.trim()) return;

    // Dodaj do listy (tu możesz podłączyć API)
    setPriceLists([...priceLists, priceListName.trim()]);
    setPriceListName('');
    closeModal();
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />

      <main className="max-w-3xl mx-auto mt-8">
        <button
          onClick={openModal}
          className="mb-6 px-5 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-xl transition"
        >
          Dodaj cennik
        </button>

        <h2 className="text-2xl font-bold mb-4 text-gray-800">Twój cennik</h2>
        <ul className="list-disc list-inside space-y-2 text-gray-700">
          {priceLists.length === 0 ? (
            <li>Brak cenników do wyświetlenia.</li>
          ) : (
            priceLists.map((name, i) => <li key={i}>{name}</li>)
          )}
        </ul>
      </main>

      {/* Modal */}
      {modalOpen && (
        <div
          className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50"
          onClick={closeModal}
        >
          <div
            className="bg-white rounded-2xl p-6 w-full max-w-md relative"
            onClick={(e) => e.stopPropagation()}
          >
            <button
              onClick={closeModal}
              className="absolute top-3 right-3 text-gray-500 hover:text-gray-700 text-2xl font-bold"
              aria-label="Close modal"
            >
              &times;
            </button>
            <h2 className="text-xl font-semibold mb-4">Dodaj Cennik</h2>

            <form onSubmit={handleSubmit} className="space-y-4">
              <input
                type="text"
                placeholder="Wpisz nazwę cennika"
                value={priceListName}
                onChange={(e) => setPriceListName(e.target.value)}
                required
                className="w-full px-3 py-2 border rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-400"
              />
              <button
                type="submit"
                className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-xl transition"
              >
                Dodaj
              </button>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default PriceList;
