import axios from 'axios';
import React, { useState } from 'react';

const AddParams: React.FC = () => {
  const [form, setForm] = useState({
    name: '',
    type: '',
    value: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });

    

  };

  const handleAddParam = async(e: React.FormEvent) => {
    e.preventDefault();
    ///const {name, type, value} = form;
    console.log('Dodano parametr:', form);
    try {
      const response = await axios.post('https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/params',
        {
          ...form
        },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
      console.log('Odpowiedź z API:', response.data);
    }
    catch (error) {
      console.error('Błąd podczas zmiany wartości:', error);
    }
    
    alert('Parametr dodany!');
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-100 via-white to-blue-200">
      <form
        onSubmit={handleAddParam}
        className="w-full max-w-md p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-4"
      >
        <h2 className="text-2xl font-bold text-center text-gray-800">Dodaj Parametr</h2>

        <input
          type="text"
          name="name"
          placeholder="Nazwa parametru"
          className="w-full p-2 border rounded-xl"
          value={form.name}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="type"
          placeholder="Rodzaj parametru"
          className="w-full p-2 border rounded-xl"
          value={form.type}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="value"
          placeholder="Wartość"
          className="w-full p-2 border rounded-xl"
          value={form.value}
          onChange={handleChange}
          required
        />

        <button
          type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 text-white p-2 rounded-xl transition"
        >
          Dodaj parametr
        </button>
      </form>
    </div>
  );
};

export default AddParams;
