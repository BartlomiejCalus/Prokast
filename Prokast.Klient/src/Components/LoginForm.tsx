import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import Cookies from 'js-cookie';

const API_URL = process.env.REACT_APP_API_URL; 

const LoginForm: React.FC = () => {
  const [login, setLogin] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError('');

    if (!login || !password) {
      setError('Wprowadź login i hasło.');
      return;
    }

    try {
      const response = await axios.post(
        `${API_URL}/api/login`,
        { login, password },
        { headers: { 'Content-Type': 'application/json' } }
      );

      const data = response.data;

      Cookies.set('token', data.token, {
        expires: 1 / 24, // 1 godzina
        secure: window.location.protocol === 'https:',
        sameSite: 'strict',
      });

      navigate('/dashboard');
    } catch (err: any) {
      if (err.response?.status === 400) {
        setError('Nieprawidłowy login lub hasło.');
      } else {
        setError('Wystąpił błąd podczas logowania.');
      }
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-green-100 via-white to-green-200">
      <form onSubmit={handleLogin} className="max-w-md w-full p-6 bg-white shadow-md rounded-2xl space-y-5">
        <h2 className="text-2xl font-bold text-center">Logowanie</h2>
        {error && <div className="text-red-500 text-sm text-center">{error}</div>}

        <input
          type="text"
          placeholder="Login"
          className="w-full p-2 border rounded-xl"
          value={login}
          onChange={(e) => setLogin(e.target.value)}
        />
        <input
          type="password"
          placeholder="Hasło"
          className="w-full p-2 border rounded-xl"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button
          type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 text-white p-2 rounded-xl transition"
        >
          Zaloguj się
        </button>

        {/* Tekstowy link do rejestracji */}
        <div className="text-center mt-2">
          <Link
            to="/RegisterForm"
            className="text-blue-500 hover:text-blue-600 font-medium transition text-sm"
          >
            Nie masz konta? Zarejestruj się
          </Link>
        </div>
      </form>
    </div>
  );
};

export default LoginForm;