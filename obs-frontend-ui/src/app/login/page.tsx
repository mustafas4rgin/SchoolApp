'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import axios from 'axios';
import { saveTokens } from '../../../lib/cookies';
import axiosInstance from '../../../lib/axiosInstance';
import { useAuth } from '../../../context/AuthContext';

export default function LoginPage() {
  const { isAuthenticated, loading, role, setAuth } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const router = useRouter();

  useEffect(() => {
    if (!loading && isAuthenticated) {
      if (role === 'Student') router.replace('/dashboard/student');
      else if (role === 'Teacher') router.replace('/dashboard/teacher');
      else router.replace('/dashboard');
    }
  }, [loading, isAuthenticated, role]);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const loginRes = await axios.post('http://localhost:5286/api/Auth/login', {
        email,
        password,
      });

      const { accessToken, refreshToken } = loginRes.data.data;
      saveTokens(accessToken, refreshToken);

      const meRes = await axiosInstance.get('/auth/me');
      const user = meRes.data;

      localStorage.setItem('me', JSON.stringify(user));
      setAuth(user); // ✅ AuthContext'i hemen güncelle

      if (user.role === 'Student') router.push('/dashboard/student');
      else if (user.role === 'Teacher') router.push('/dashboard/teacher');
      else setError('Geçersiz kullanıcı rolü.');
    } catch (err: any) {
      const msg = err?.response?.data?.message || 'Giriş başarısız.';
      setError(msg);
    }
  };

  if (loading || isAuthenticated) return null;

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-100 px-4">
      <div className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
        <h2 className="mb-6 text-center text-2xl font-bold text-gray-800">OBS Giriş</h2>
        <form onSubmit={handleLogin} className="space-y-4">
          <input
            type="email"
            placeholder="Email"
            className="w-full rounded border px-4 py-2"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Şifre"
            className="w-full rounded border px-4 py-2"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          {error && <p className="text-sm text-red-500">{error}</p>}
          <button
            type="submit"
            className="w-full rounded bg-blue-600 py-2 text-white hover:bg-blue-700"
          >
            Giriş Yap
          </button>
        </form>
      </div>
    </div>
  );
}
