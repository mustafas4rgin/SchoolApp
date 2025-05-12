'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';

interface Grade {
  courseName: string;
  midterm: number;
  final: number;
}

interface Course {
  courseName: string;
  teacherName: string;
  attendance: number;
}

interface Student {
  firstName: string;
  lastName: string;
  number: string;
  email: string;
  phone: string;
  departmentName: string | null;
  roleName: string | null;
  grades: Grade[];
}

const StudentInfoPage = () => {
  const [student, setStudent] = useState<Student | null>(null);
  const [courses, setCourses] = useState<Course[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [studentRes, coursesRes] = await Promise.all([
          axiosInstance.get('/Student/MyInfo?Include=all'),
          axiosInstance.get('/StudentCourse/CurrentYearsCourses'),
        ]);
        setStudent(studentRes.data);
        setCourses(coursesRes.data);
      } catch (err) {
        console.error('Veriler alınamadı:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

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

  return (
    <div className="p-8">
      <h1 className="text-2xl font-bold text-gray-800 mb-6">👤 Öğrenci Bilgileri</h1>

      {loading ? (
        <p className="text-gray-500">Yükleniyor...</p>
      ) : !student ? (
        <p className="text-red-500">Bilgiler alınamadı.</p>
      ) : (
        <>
          {/* Kişisel Bilgiler */}
          <div className="bg-white rounded-2xl shadow p-6 border border-gray-200 space-y-2 text-gray-700 text-sm mb-6">
            <p><strong>Ad Soyad:</strong> {student.firstName} {student.lastName}</p>
            <p><strong>Öğrenci No:</strong> {student.number}</p>
            <p><strong>Email:</strong> {student.email}</p>
            <p><strong>Telefon:</strong> {student.phone}</p>
            <p><strong>Bölüm:</strong> {student.departmentName ?? 'Bilinmiyor'}</p>
            <p><strong>Rol:</strong> {student.roleName ?? 'Öğrenci'}</p>
          </div>

          {/* Bu Yılın Dersleri */}
          <h2 className="text-xl font-semibold text-gray-800 mb-3">📘 Bu Yılın Dersleri</h2>
          <div className="overflow-x-auto bg-white rounded-2xl shadow p-4 border border-gray-200">
            <table className="w-full border-collapse text-sm text-gray-700">
              <thead className="bg-gray-100">
                <tr>
                  <th className="py-2 px-4 text-left">Ders Adı</th>
                  <th className="py-2 px-4 text-left">Hoca</th>
                  <th className="py-2 px-4 text-left">Vize</th>
                  <th className="py-2 px-4 text-left">Final</th>
                  <th className="py-2 px-4 text-left">Ortalama</th>
                  <th className="py-2 px-4 text-left">Harf Notu</th>
                  <th className="py-2 px-4 text-left">Devamsızlık</th>
                </tr>
              </thead>
              <tbody>
                {courses.map((course, index) => {
                  const grade = student.grades.find(g => g.courseName === course.courseName);
                  const midterm = grade?.midterm ?? 0;
                  const final = grade?.final ?? 0;
                  const average = final > 0 ? (midterm * 0.4 + final * 0.6).toFixed(1) : '-';
                  const letter = final > 0 ? getLetter(Number(average)) : '-';

                  return (
                    <tr key={index} className="border-t">
                      <td className="py-2 px-4">{course.courseName}</td>
                      <td className="py-2 px-4">{course.teacherName}</td>
                      <td className="py-2 px-4">{midterm}</td>
                      <td className="py-2 px-4">
                        {final > 0 ? final : <span className="italic text-gray-500">Girilmedi</span>}
                      </td>
                      <td className="py-2 px-4">{average}</td>
                      <td className="py-2 px-4 font-medium">{letter}</td>
                      <td className="py-2 px-4">%{course.attendance}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </>
      )}
    </div>
  );
};

export default StudentInfoPage;
