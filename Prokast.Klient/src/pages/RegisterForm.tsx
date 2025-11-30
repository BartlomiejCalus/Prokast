import axios from 'axios';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
//import Navbar from '../Components/Navbar';

const API_URL = process.env.REACT_APP_API_URL; 

const RegisterForm: React.FC = () => {
  const [form, setForm] = useState({
    login: '',
    password: '',
    firstName: '',
    lastName: '',
    businessName: '',
    nip: '',
    address: '',
    phoneNumber: '',
    postalCode: '',
    city: '',
    country: ''
  });

  const normalizeInput = (name: string, value: string) => {
    switch (name) {
      case 'nip': {
        const cleaned = value.toUpperCase().replace(/[^A-Z0-9]/g, '');
        const parts = [
          cleaned.slice(0, 2),      // WS
          cleaned.slice(2, 5),      // 017
          cleaned.slice(5, 11),     // 240378
          cleaned.slice(11, 12)     // 1
        ];
        return parts.filter(Boolean).join('-');
      }

      case 'phoneNumber': {
        const digits = value.replace(/\D/g, '').slice(0, 9);
        if (digits.length < 4) return digits;
        if (digits.length <= 6)
          return `${digits.slice(0, 3)}-${digits.slice(3)}`;
        return `${digits.slice(0, 3)}-${digits.slice(3, 6)}-${digits.slice(6)}`;
      }

      case 'postalCode': {
        const digits = value.replace(/\D/g, '').slice(0, 5);
        if (digits.length >= 3)
          return `${digits.slice(0, 2)}-${digits.slice(2)}`;
        return digits;
      }

      case 'firstName':
      case 'lastName':
      case 'city':
      case 'country':
        return value.charAt(0).toUpperCase() + value.slice(1).toLowerCase();

      case 'login':
      case 'address':
      case 'businessName':
        return value.trim();

      default:
        return value;
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    const normalizedValue = normalizeInput(name, value);
    setForm({ ...form, [name]: normalizedValue });
  };

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        `${API_URL}/api/client`,
        { ...form },
        {
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'text/plain'
          }
        }
      );

      if (response.status >= 200 && response.status <= 204) {
        console.log('Rejestracja zakończona sukcesem!');
      }

      console.log('Odpowiedź z API:', response.data);
    } catch (error: any) {
      console.error('Błąd podczas rejestracji:', error);
      if (error.response) {
        console.error('Kod odpowiedzi:', error.response.status);
        console.error('Treść błędu:', error.response.data);
      }
    }
  };

  return (
    <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
      <main className="flex flex-col items-center justify-center w-screen mt-10">
      <form onSubmit={handleRegister} className="w-full max-w-md p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-2">
        <h2 className="text-2xl font-bold text-center text-gray-800">Rejestracja</h2>

        <input
          type="text"
          name="login"
          placeholder="Login"
          className="w-full p-2 border rounded-xl"
          value={form.login}
          onChange={handleChange}
          required
        />

        <input
          type="password"
          name="password"
          placeholder="Hasło"
          className="w-full p-2 border rounded-xl"
          value={form.password}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="firstName"
          placeholder="Imię"
          className="w-full p-2 border rounded-xl"
          value={form.firstName}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="lastName"
          placeholder="Nazwisko"
          className="w-full p-2 border rounded-xl"
          value={form.lastName}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="businessName"
          placeholder="Nazwa firmy"
          className="w-full p-2 border rounded-xl"
          value={form.businessName}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="nip"
          placeholder="NIP"
          className="w-full p-2 border rounded-xl"
          value={form.nip}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="address"
          placeholder="Adres"
          className="w-full p-2 border rounded-xl"
          value={form.address}
          onChange={handleChange}
          required
        />

        <input
          type="tel"
          name="phoneNumber"
          placeholder="Numer telefonu"
          className="w-full p-2 border rounded-xl"
          value={form.phoneNumber}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="postalCode"
          placeholder="Kod pocztowy"
          className="w-full p-2 border rounded-xl"
          value={form.postalCode}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="city"
          placeholder="Miasto"
          className="w-full p-2 border rounded-xl"
          value={form.city}
          onChange={handleChange}
          required
        />

        <input
          type="text"
          name="country"
          placeholder="Państwo"
          className="w-full p-2 border rounded-xl"
          value={form.country}
          onChange={handleChange}
          required
        />

        <button
          type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 text-white p-2 rounded-xl transition"
        >
          Stwórz konto
        </button>
      </form>
      </main>
    </div>
  );
};

export default RegisterForm;
