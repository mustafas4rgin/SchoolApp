'use client';
import { useAuth } from '../../../context/AuthContext'; // kendi yoluna göre güncelle
import { FaSignOutAlt } from 'react-icons/fa';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import {
  FaHome,
  FaGraduationCap,
  FaBoxes,
} from 'react-icons/fa';

export default function Sidebar() {
  const pathname = usePathname();
  const { logout } = useAuth();
  const studentMenu = [
    { label: 'Ders Seçimi-Kayıt Yenileme', href: '/dashboard/student/course-selection' },
    { label: 'Öğrenci Bilgi Ekranı', href: '/dashboard/student/info' },
    { label: 'Burs Başvurusu', href: '/dashboard/student/scholarship' },
    { label: 'Eğitim Ücretleri İşlemleri', href: '/dashboard/student/fees' },
  ];

  return (
    <aside className="w-64 h-screen bg-gray-900 text-white fixed left-0 top-0 flex flex-col z-40">
      <div className="flex items-center justify-center h-16 border-b border-gray-700 px-4">
        <img src="/logo.png" alt="logo" className="h-10 mr-2" />
        <h1 className="text-sm font-semibold text-center leading-5">
          ANTALYA SIMOSH<br />UNIVERSITY
        </h1>
      </div>

      <nav className="flex-1 overflow-y-auto p-4 space-y-4">
        {/* Ana sayfa */}
        <Link href="/dashboard/student">
          <div className={`flex items-center gap-3 px-3 py-2 rounded-md cursor-pointer transition-all duration-200
              ${pathname === '/dashboard/student'
              ? 'bg-blue-600 text-white'
              : 'text-gray-300 hover:bg-gray-800 hover:text-white'
            }`}>
            <FaHome className="text-lg" />
            <span className="text-sm font-medium">Ev</span>
          </div>
        </Link>

        {/* Öğrenci Menüsü */}
        <div className="mt-6">
          <div className="flex items-center gap-2 mb-2 text-sm font-semibold text-gray-400 uppercase tracking-wide">
            <FaGraduationCap />
            Öğrenci Menüsü
          </div>
          <ul className="ml-2 space-y-1">
            {studentMenu.map((item) => (
              <li key={item.href}>
                <Link href={item.href}>
                  <span className={`block px-3 py-2 rounded-md text-sm cursor-pointer transition-all duration-200
                    ${pathname === item.href
                      ? 'bg-blue-600 text-white'
                      : 'text-gray-300 hover:bg-gray-800 hover:text-white'
                    }`}>
                    {item.label}
                  </span>
                </Link>
              </li>
            ))}
          </ul>
        </div>

        {/* Anketler */}
        <div className="mt-6">
          <div className="flex items-center gap-2 mb-2 text-sm font-semibold text-gray-400 uppercase tracking-wide">
            <FaBoxes />
            Anketler
          </div>
          <Link href="/dashboard/student/surveys">
            <span className={`block px-3 py-2 rounded-md text-sm cursor-pointer transition-all duration-200
              ${pathname === '/dashboard/student/surveys'
                ? 'bg-blue-600 text-white'
                : 'text-gray-300 hover:bg-gray-800 hover:text-white'
              }`}>
              Anket Sayfası
            </span>
          </Link>
        </div>
        <div className="mt-auto p-4 border-t border-gray-700">
          <button
            onClick={logout}
            className="w-full flex items-center justify-center gap-2 bg-red-600 hover:bg-red-700 text-white py-2 px-4 rounded-md text-sm font-medium transition"
          >
            <FaSignOutAlt />
            Çıkış Yap
          </button>
        </div>
      </nav>
    </aside>
  );
}
