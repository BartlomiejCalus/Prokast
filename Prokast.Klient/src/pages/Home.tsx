import React from 'react';
import LoginForm from '../Components/LoginForm';

const Home: React.FC = () => {
  return (
    <div className="min-w-full min-h-screen bg-gray-100 ">
      <LoginForm />
    </div>
  );
};

export default Home;