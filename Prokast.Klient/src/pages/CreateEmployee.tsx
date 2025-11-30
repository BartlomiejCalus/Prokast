import React, { useState } from 'react';
import Navbar from '../Components/Navbar';

const API_URL = process.env.REACT_APP_API_URL; 

const CreateEmployee: React.FC = () => {
  const [response, setResponse] = useState('');

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const form = e.currentTarget;
    const formData = new FormData(form);

    const data = {
      firstName: formData.get('firstName'),
      lastName: formData.get('lastName'),
      email: formData.get('email'),
      warehouseID: formData.get('warehouseID'),
      role: formData.get('role')
    };

    setResponse(JSON.stringify(data, null, 2));
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-green-100 via-white to-green-200 p-4">
      <Navbar />
      <div className="max-w-2xl mx-auto bg-white/80 backdrop-blur-md shadow-xl rounded-2xl p-6 mt-8">
        <h2 className="text-2xl font-bold text-center text-gray-800 mb-6">Stwórz konto pracownika</h2>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-gray-700">Nazwa użytkownika:</label>
            <input type="text" name="firstName" required className="w-full p-2 border rounded-xl" />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Hasło:</label>
            <input type="password" name="lastName" required className="w-full p-2 border rounded-xl" />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Email:</label>
            <input type="email" name="email" required className="w-full p-2 border rounded-xl" />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">ID magazynu:</label>
            <input type="number" name="warehouseID" required className="w-full p-2 border rounded-xl" />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Rola:</label>
            <input type="number" name="role" required className="w-full p-2 border rounded-xl" />
          </div>

          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-600 text-white font-semibold px-4 py-2 rounded-xl transition"
          >
            Stwórz konto
          </button>
        </form>

        <h3 className="text-lg font-semibold mt-6 text-gray-800">Dane pracownika:</h3>
        <pre className="mt-2 bg-gray-100 p-4 rounded-xl text-sm">{response}</pre>
      </div>
    </div>
  );
};

export default CreateEmployee;
