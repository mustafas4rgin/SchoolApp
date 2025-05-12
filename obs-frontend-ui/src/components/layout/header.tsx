'use client';

import { useAuth } from '../../../context/AuthContext';

export default function Header() {
  const { firstName, role, logout } = useAuth();

  return (
    <header className="flex items-center justify-between bg-blue-600 px-6 py-4 text-white shadow">
      <h1 className="text-xl font-bold">OBS Uygulaması</h1>
      <div className="flex items-center gap-4">
        {firstName && <span>{firstName} ({role})</span>}
        <button
          onClick={logout}
          className="rounded bg-white px-4 py-1 text-blue-600 hover:bg-gray-100"
        >
          Çıkış Yap
        </button>
      </div>
    </header>
  );
}
