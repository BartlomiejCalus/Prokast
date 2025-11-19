import React, { useEffect, useState } from 'react';
import Navbar from '../Components/Navbar';

const API_URL = process.env.REACT_APP_API_URL; 

const DictionaryParams: React.FC = () => {
  const [regionOptions, setRegionOptions] = useState<string[]>([]);
  const [paramOptions, setParamOptions] = useState<string[]>([]);
  const [valueOptions, setValueOptions] = useState<string[]>([]);

  const [selectedRegion, setSelectedRegion] = useState('');
  const [selectedParam, setSelectedParam] = useState('');
  const [selectedValue, setSelectedValue] = useState('');

  useEffect(() => {
    // Załaduj dane (na razie statycznie, można podpiąć API)
    setRegionOptions(['Europa', 'Ameryka', 'Azja']);
    setParamOptions(['Kolor', 'Rozmiar', 'Materiał']);
    setValueOptions(['Czerwony', 'Zielony', 'Niebieski']);
  }, []);

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />
      <div className="max-w-xl mx-auto bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 mt-8">
        <h1 className="text-2xl font-bold text-center mb-6 text-gray-800">Parametry słownikowe</h1>

        <label className="block mb-2 font-medium text-gray-700" htmlFor="region">Wybierz region:</label>
        <select
          id="region"
          className="w-full p-2 mb-4 border rounded-xl"
          value={selectedRegion}
          onChange={(e) => setSelectedRegion(e.target.value)}
        >
          <option value="">-- Wybierz --</option>
          {regionOptions.map((option, index) => (
            <option key={index} value={option}>{option}</option>
          ))}
        </select>

        <label className="block mb-2 font-medium text-gray-700" htmlFor="param">Wybierz parametr:</label>
        <select
          id="param"
          className="w-full p-2 mb-4 border rounded-xl"
          value={selectedParam}
          onChange={(e) => setSelectedParam(e.target.value)}
        >
          <option value="">-- Wybierz --</option>
          {paramOptions.map((option, index) => (
            <option key={index} value={option}>{option}</option>
          ))}
        </select>

        <label className="block mb-2 font-medium text-gray-700" htmlFor="name">Wybierz wartość:</label>
        <select
          id="name"
          className="w-full p-2 mb-4 border rounded-xl"
          value={selectedValue}
          onChange={(e) => setSelectedValue(e.target.value)}
        >
          <option value="">-- Wybierz --</option>
          {valueOptions.map((option, index) => (
            <option key={index} value={option}>{option}</option>
          ))}
        </select>
      </div>
    </div>
  );
};

export default DictionaryParams;
