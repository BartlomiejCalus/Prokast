import React from 'react';
import { Navigate } from 'react-router-dom';
import Cookies from 'js-cookie';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ children }) => {
  // Sprawdzamy WYŁĄCZNIE ciasteczko z tokenem
  const token = Cookies.get('token');

  if (!token) {
    // Jeśli nie ma tokenu → przekieruj na login
    return <Navigate to="/" replace />;
  }

  // Jeśli token istnieje → pozwól przejść dalej
  return <>{children}</>;
};

export default ProtectedRoute;