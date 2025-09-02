import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const LoginForm: React.FC = () => {
  const [login, setlogin] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!login || !password) {
      setError('Wprowadź login i hasło.');
      return;
    }

    try {
      const response = await axios.post(
        'https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/login',
        {
          login: login,
          password: password,
        },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      const data = response.data;
      console.log('Odpowiedź z API:', data);

    

      navigate('/dashboard');
    } catch (err: any) {
      console.error(err);
      if (err.response && err.response.status === 401) {
        setError('Nieprawidłowy login lub hasło.');
      } else {
        setError('Wystąpił błąd podczas logowania.');
      }
    }
  };

  return (
    <div className='min-h-screen min-w-full flex items-center justify-center bg-gradient-to-br from-blue-100 via-white to-blue-200'>
      <form onSubmit={handleLogin} className="max-w-md mx-auto mt-20 p-6 bg-white shadow-md rounded-2xl space-y-5">
        <h2 className="text-2xl font-bold text-center">Logowanie</h2>

        {error && <div className="text-red-500 text-sm text-center">{error}</div>}

        <input
          type="login"
          placeholder="login"
          className="w-full p-2 border rounded-xl"
          value={login}
          onChange={(e) => setlogin(e.target.value)}
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
      </form>
    </div>
  );
};

export default LoginForm;
