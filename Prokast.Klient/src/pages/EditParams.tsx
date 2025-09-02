import React, { useEffect, useState } from 'react';
import Navbar from '../Components/Navbar';

const EditParams: React.FC = () => {
  const [params, setParams] = useState<string[]>([]);
  const [selectedParam, setSelectedParam] = useState('');
  const [type, setType] = useState('');
  const [value, setValue] = useState('');

  useEffect(() => {
 
    setParams(['Rozmiar', 'Kolor', 'Materiał']);
  }, []);

  const handleSave = () => {
 
    console.log(`Zapisz: ${selectedParam}, ${type}, ${value}`);
  };

  const handleDelete = () => {
   
    console.log(`Usuń: ${selectedParam}`);
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />
      <div className="max-w-xl mx-auto bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 mt-8">
        <h1 className="text-2xl font-bold text-center text-gray-800 mb-6">Edytuj Parametr</h1>

        <label className="block mb-2 font-medium text-gray-700">Wybierz parametr do edycji:</label>
        <select
          className="w-full p-2 mb-4 border rounded-xl"
          value={selectedParam}
          onChange={(e) => setSelectedParam(e.target.value)}
        >
          <option value="">-- Wybierz --</option>
          {params.map((param, index) => (
            <option key={index} value={param}>{param}</option>
          ))}
        </select>

        <h2 className="text-xl font-semibold text-gray-700 mt-6 mb-4">Edycja Parametru</h2>

        <label className="block mb-2 font-medium text-gray-700">Rodzaj:</label>
        <input
          type="text"
          className="w-full p-2 mb-4 border rounded-xl"
          placeholder="Rodzaj parametru"
          value={type}
          onChange={(e) => setType(e.target.value)}
        />

        <label className="block mb-2 font-medium text-gray-700">Wartość:</label>
        <input
          type="text"
          className="w-full p-2 mb-6 border rounded-xl"
          placeholder="Wartość parametru"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />

        <div className="flex justify-between">
          <button
            onClick={handleSave}
            className="bg-blue-500 hover:bg-blue-600 text-white font-semibold px-4 py-2 rounded-xl transition"
          >
            Zapisz zmiany
          </button>
          <button
            onClick={handleDelete}
            className="bg-red-500 hover:bg-red-600 text-white font-semibold px-4 py-2 rounded-xl transition"
          >
            Usuń parametr
          </button>
        </div>
      </div>
    </div>
  );
};

export default EditParams;
