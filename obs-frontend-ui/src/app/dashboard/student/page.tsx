'use client';

import { useEffect, useState } from 'react';
import { useAuth } from '../../../../context/AuthContext'; 
import { useRouter } from 'next/navigation';
import axiosInstance from '../../../../lib/axiosInstance';

interface Grade {
  courseName: string;
  midterm: number;
  final: number;
}

interface Course {
  courseName: string;
  attendance: number;
}

interface Student {
  firstName: string;
  lastName: string;
  number: string;
  departmentName: string | null;
  email: string;
  phone: string;
  grades: Grade[];
}

const StudentDashboardPage = () => {
  const { isAuthenticated, loading } = useAuth();
  const router = useRouter();
  const [student, setStudent] = useState<Student | null>(null);
  const [courses, setCourses] = useState<Course[]>([]);
  const [dataLoading, setDataLoading] = useState(true);

  useEffect(() => {
    if (!loading && !isAuthenticated) {
      router.push('/login');
    }
  }, [loading, isAuthenticated]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [studentRes, courseRes] = await Promise.all([
          axiosInstance.get('/Student/MyInfo?Include=all'),
          axiosInstance.get('/StudentCourse/CurrentYearsCourses'),
        ]);
        setStudent(studentRes.data);
        setCourses(courseRes.data);
      } catch (err) {
        console.error('Dashboard verileri alınamadı:', err);
      } finally {
        setDataLoading(false);
      }
    };

    if (isAuthenticated) {
      fetchData();
    }
  }, [isAuthenticated]);

  const getLetter = (avg: number): string => {
    if (avg >= 90) return 'AA';
    if (avg >= 85) return 'BA';
    if (avg >= 75) return 'BB';
    if (avg >= 65) return 'CB';
    if (avg >= 55) return 'CC';
    if (avg >= 50) return 'DC';
    if (avg >= 45) return 'DD';
    return 'FF';
  };

  if (loading || !isAuthenticated || dataLoading) {
    return <p className="p-8 text-gray-500">Yükleniyor...</p>;
  }

  return (
    <div className="p-6 space-y-6">
      {/* Öğrenci Bilgisi */}
      <div className="bg-white shadow-md border border-gray-200 rounded-2xl p-6">
        <h2 className="text-xl font-bold text-gray-800 mb-1">
          👋 Hoş geldin, {student?.firstName} {student?.lastName}
        </h2>
        <div className="text-gray-600 text-sm space-y-1 mt-2">
          <p><strong>Öğrenci No:</strong> {student?.number}</p>
          <p><strong>Email:</strong> {student?.email}</p>
          <p><strong>Telefon:</strong> {student?.phone}</p>
          <p><strong>Bölüm:</strong> {student?.departmentName ?? 'Bilinmiyor'}</p>
        </div>
      </div>

      {/* Bu Yılın Dersleri */}
      <div className="bg-white shadow-md border border-gray-200 rounded-2xl p-6">
        <h3 className="text-lg font-semibold text-gray-800 mb-3">📚 Aktif Yıl Dersleri</h3>
        <table className="w-full text-sm text-gray-700 border-collapse">
          <thead className="bg-gray-100">
            <tr>
              <th className="py-2 px-4 text-left">Ders</th>
              <th className="py-2 px-4 text-left">Devamsızlık</th>
              <th className="py-2 px-4 text-left">Vize</th>
              <th className="py-2 px-4 text-left">Final</th>
              <th className="py-2 px-4 text-left">Ortalama</th>
              <th className="py-2 px-4 text-left">Harf</th>
            </tr>
          </thead>
          <tbody>
            {courses.map((course, index) => {
              const grade = student?.grades.find((g) => g.courseName === course.courseName);
              const midterm = grade?.midterm ?? 0;
              const final = grade?.final ?? 0;
              const average = final > 0 ? (midterm * 0.4 + final * 0.6).toFixed(1) : '-';
              const letter = final > 0 ? getLetter(Number(average)) : '-';

              const letterColor = () => {
                if (letter === 'AA' || letter === 'BA') return 'text-green-600';
                if (letter === 'BB' || letter === 'CB') return 'text-yellow-600';
                if (letter === 'CC' || letter === 'DC' || letter === 'DD') return 'text-orange-500';
                if (letter === 'FF') return 'text-red-600';
                return 'text-gray-600';
              };

              return (
                <tr key={index} className="border-t">
                  <td className="py-2 px-4">{course.courseName}</td>
                  <td className="py-2 px-4">%{course.attendance}</td>
                  <td className="py-2 px-4">{midterm}</td>
                  <td className="py-2 px-4">
                    {final > 0 ? final : <span className="italic text-gray-500">Girilmedi</span>}
                  </td>
                  <td className="py-2 px-4">{average}</td>
                  <td className={`py-2 px-4 font-semibold ${letterColor()}`}>{letter}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default StudentDashboardPage;
