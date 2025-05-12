'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';

interface Course {
  id: number;
  name: string;
  teacherName: string;
  credit: number;
  weeklyHours: number;
  year: number;
}

interface UnconfirmedCourse {
  id: number;
  courseName: string;
  studentName: string;
  attendance: number;
  joinDate: string;
  isConfirmed: boolean;
}

const CourseSelectionPage = () => {
  const [courses, setCourses] = useState<Course[]>([]);
  const [selectedCourseIds, setSelectedCourseIds] = useState<number[]>([]);
  const [unconfirmedCourses, setUnconfirmedCourses] = useState<UnconfirmedCourse[]>([]);
  const [unconfirmedError, setUnconfirmedError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [confirming, setConfirming] = useState(false);
  const [activeYear, setActiveYear] = useState(1);

  useEffect(() => {
    fetchCourses();
    fetchUnconfirmedCourses();
  }, []);

  const fetchCourses = async () => {
    try {
      const res = await axiosInstance.get('/Course/AvailableCourses?include=all');
      setCourses(res.data);
    } catch (err) {
      console.error('Dersler alınamadı:', err);
    } finally {
      setLoading(false);
    }
  };

  const fetchUnconfirmedCourses = async () => {
    try {
      const res = await axiosInstance.get('/StudentCourse/Unconfirmed?Include=all');
      setUnconfirmedCourses(res.data);
      setUnconfirmedError(null);
    } catch (err: any) {
      if (err.response?.status === 404) {
        setUnconfirmedCourses([]);
        setUnconfirmedError('Onay bekleyen ders kaydınız yok.');
      } else {
        console.error('Onaylanmamış dersler alınamadı:', err);
        setUnconfirmedError('Bir hata oluştu. Lütfen tekrar deneyin.');
      }
    }
  };

  const toggleSelect = (courseId: number) => {
    setSelectedCourseIds((prev) =>
      prev.includes(courseId)
        ? prev.filter((id) => id !== courseId)
        : [...prev, courseId]
    );
  };

  const handleSubmit = async () => {
    if (selectedCourseIds.length === 0) return;
    setSubmitting(true);
    try {
      await axiosInstance.post('/StudentCourse/SelectCourses', {
        courseIds: selectedCourseIds,
      });
      alert('✅ Ders seçimi kaydedildi.');
      setSelectedCourseIds([]);
      fetchUnconfirmedCourses();
    } catch (err: any) {
      const msg = err.response?.data?.message || '❌ Bir hata oluştu.';
      alert(msg);
    } finally {
      setSubmitting(false);
    }
  };

  const handleConfirm = async () => {
    setConfirming(true);
    try {
      await axiosInstance.put('/StudentCourse/ConfirmSelection');
      alert('✅ Seçiminiz onaylandı.');
      setUnconfirmedCourses([]);
    } catch (err) {
      console.error('Onay hatası:', err);
      alert('❌ Dersler onaylanamadı.');
    } finally {
      setConfirming(false);
    }
  };

  const filteredCourses = courses.filter((c) => c.year === activeYear);

  return (
    <div className="p-8 space-y-10">
      <h1 className="text-2xl font-bold text-gray-800">📚 Ders Seçimi</h1>

      <div className="flex gap-2">
        {[1, 2, 3, 4].map((year) => (
          <button
            key={year}
            onClick={() => setActiveYear(year)}
            className={`px-4 py-1 rounded-full text-sm font-medium border transition ${
              activeYear === year
                ? 'bg-blue-600 text-white border-blue-600'
                : 'bg-white text-gray-700 border-gray-300 hover:bg-gray-100'
            }`}
          >
            {year}. Sınıf Dersleri
          </button>
        ))}
      </div>

      {loading ? (
        <p className="text-gray-500">Dersler yükleniyor...</p>
      ) : filteredCourses.length === 0 ? (
        <p className="text-gray-500">Bu sınıfa ait ders bulunamadı.</p>
      ) : (
        <div className="overflow-x-auto bg-white shadow rounded-2xl border border-gray-200">
          <table className="w-full text-sm text-gray-700">
            <thead className="bg-gray-100">
              <tr>
                <th className="py-2 px-4 text-left">Ders</th>
                <th className="py-2 px-4 text-left">Hoca</th>
                <th className="py-2 px-4 text-left">Kredi</th>
                <th className="py-2 px-4 text-left">Saat</th>
                <th className="py-2 px-4 text-left">Seç</th>
              </tr>
            </thead>
            <tbody>
              {filteredCourses.map((course) => (
                <tr key={course.id} className="border-t">
                  <td className="py-2 px-4">{course.name}</td>
                  <td className="py-2 px-4">{course.teacherName}</td>
                  <td className="py-2 px-4">{course.credit}</td>
                  <td className="py-2 px-4">{course.weeklyHours}</td>
                  <td className="py-2 px-4">
                    <input
                      type="checkbox"
                      checked={selectedCourseIds.includes(course.id)}
                      onChange={() => toggleSelect(course.id)}
                    />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {selectedCourseIds.length > 0 && (
        <div>
          <button
            onClick={handleSubmit}
            disabled={submitting}
            className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-xl shadow disabled:opacity-50"
          >
            {submitting ? 'Kaydediliyor...' : 'Dersleri Kaydet'}
          </button>
        </div>
      )}

      {unconfirmedError ? (
        <p className="text-sm text-gray-500 italic">{unconfirmedError}</p>
      ) : unconfirmedCourses.length > 0 && (
        <div className="mt-10 space-y-4">
          <h2 className="text-xl font-semibold text-gray-800">📝 Onaylanmamış Dersler</h2>

          <div className="overflow-x-auto bg-white shadow rounded-2xl border border-gray-200">
            <table className="w-full text-sm text-gray-700">
              <thead className="bg-gray-100">
                <tr>
                  <th className="py-2 px-4 text-left">Ders</th>
                  <th className="py-2 px-4 text-left">Öğrenci</th>
                  <th className="py-2 px-4 text-left">Devamsızlık</th>
                  <th className="py-2 px-4 text-left">Kayıt Tarihi</th>
                </tr>
              </thead>
              <tbody>
                {unconfirmedCourses.map((course) => (
                  <tr key={course.id} className="border-t">
                    <td className="py-2 px-4">{course.courseName}</td>
                    <td className="py-2 px-4">{course.studentName}</td>
                    <td className="py-2 px-4">%{course.attendance}</td>
                    <td className="py-2 px-4">
                      {new Date(course.joinDate).toLocaleDateString('tr-TR', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                      })}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <button
            onClick={handleConfirm}
            disabled={confirming}
            className="bg-green-600 hover:bg-green-700 text-white px-6 py-2 rounded-xl shadow disabled:opacity-50"
          >
            {confirming ? 'Onaylanıyor...' : 'Seçimi Onayla'}
          </button>
        </div>
      )}
    </div>
  );
};

export default CourseSelectionPage;
