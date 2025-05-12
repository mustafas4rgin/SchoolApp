'use client';

import { createContext, useContext, useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { parseCookies, destroyCookie } from 'nookies';
import axiosInstance from '../lib/axiosInstance';

interface AuthContextType {
  firstName: string | null;
  role: string | null;
  isAuthenticated: boolean;
  loading: boolean;
  logout: () => void;
  setAuth: (user: { firstName: string; role: string }) => void;
}

const AuthContext = createContext<AuthContextType>({
  firstName: null,
  role: null,
  isAuthenticated: false,
  loading: true,
  logout: () => {},
  setAuth: () => {},
});

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [firstName, setFirstName] = useState<string | null>(null);
  const [role, setRole] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  const setAuth = (user: { firstName: string; role: string }) => {
    setFirstName(user.firstName);
    setRole(user.role);
    setIsAuthenticated(true);
  };

  useEffect(() => {
    const { accessToken } = parseCookies();

    if (!accessToken) {
      setLoading(false);
      return;
    }

    const cachedMe = localStorage.getItem('me');
    if (cachedMe) {
      const user = JSON.parse(cachedMe);
      setAuth(user);
      setLoading(false);
      return;
    }

    axiosInstance
      .get('/auth/me')
      .then((res) => {
        setAuth(res.data);
        localStorage.setItem('me', JSON.stringify(res.data));
      })
      .catch(() => {
        logout();
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  const logout = async () => {
    try {
      await axiosInstance.post('/auth/logout');
    } catch (err) {
      console.error('Logout isteği başarısız:', err);
    }

    destroyCookie(null, 'accessToken');
    destroyCookie(null, 'refreshToken');
    localStorage.removeItem('me');
    setFirstName(null);
    setRole(null);
    setIsAuthenticated(false);

    setTimeout(() => {
      router.push('/login');
    }, 50);
  };

  return (
    <AuthContext.Provider
      value={{ firstName, role, isAuthenticated, loading, logout, setAuth }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
