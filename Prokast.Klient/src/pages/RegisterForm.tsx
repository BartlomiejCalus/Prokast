import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../Components/Navbar';

const RegisterForm: React.FC = () => {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: '',
    businessName: '',
    nip: '',
    address: '',
    phone: '',
    postalCode: '',
    city: '',
    country: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleRegister = (e: React.FormEvent) => {
    e.preventDefault();

    if (form.password !== form.confirmPassword) {
      alert('Hasła się nie zgadzają');
      return;
    }

    navigate('/dashboard');
  };

  return (
    <div className="min-h-screen flex flex-col  bg-gradient-to-br from-blue-100 via-white to-blue-200">
      <Navbar />
      <main className="flex flex-col items-center justify-center w-screen mt-10">
      <form onSubmit={handleRegister} className="w-full max-w-md p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-2">
        <h2 className="text-2xl font-bold text-center text-gray-800">Rejestracja</h2>

        <input
          type="email"
          name="email"
          placeholder="Email"
          className="w-full p-2 border rounded-xl"
          value={form.email}
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
          type="password"
          name="confirmPassword"
          placeholder="Powtórz hasło"
          className="w-full p-2 border rounded-xl"
          value={form.confirmPassword}
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
          name="phone"
          placeholder="Numer telefonu"
          className="w-full p-2 border rounded-xl"
          value={form.phone}
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
