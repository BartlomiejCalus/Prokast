import React from 'react';
import Navbar from '../Components/Navbar';

const Dashboard: React.FC = () => {
  return (
    <div className="min-w-full min-h-screen text-center bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />
      <h1 className="text-3xl font-bold mt-10">Witaj na stronie użytkownika!</h1>
    </div>
  );
};

export default Dashboard;
