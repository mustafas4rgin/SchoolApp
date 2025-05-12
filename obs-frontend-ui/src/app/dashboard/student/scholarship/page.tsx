'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';

interface Grade {
  midterm: number;
  final: number;
  courseName: string;
}

const ScholarshipApplicationPage = () => {
  const [form, setForm] = useState({
    fullName: '',
    studentNumber: '',
    email: '',
    phone: '',
    incomeStatus: '',
    siblingCount: '',
    note: '',
    studentId: 0,
    grades: [] as Grade[],
  });

  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    const fetchStudentData = async () => {
      try {
        const res = await axiosInstance.get('/Student/MyInfo?Include=all');
        const student = res.data;

        setForm(prev => ({
          ...prev,
          fullName: `${student.firstName} ${student.lastName}`,
          studentNumber: student.number,
          email: student.email,
          phone: student.phone,
          studentId: student.id,
          grades: student.grades
        }));
      } catch (err) {
        console.error('Öğrenci verileri alınamadı:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchStudentData();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    try {
      const payload = {
        ...form,
        siblingCount: Number(form.siblingCount),
      };

      console.log('Gönderilen veri:', payload); // Hata ayıklama için
      await axiosInstance.post('/ScholarshipApplication/Add', payload);
      alert('🎉 Başvurunuz başarıyla gönderildi!');
    } catch (err) {
      alert('❌ Başvuru sırasında hata oluştu.');
      console.error(err);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) return <p className="p-8 text-gray-500">Yükleniyor...</p>;

  return (
    <div className="p-8 max-w-xl mx-auto">
      <h1 className="text-2xl font-bold text-gray-800 mb-6">🎓 Burs Başvurusu</h1>
      <form onSubmit={handleSubmit} className="space-y-4 bg-white p-6 shadow rounded-2xl border border-gray-200">
        <input type="text" name="fullName" value={form.fullName} disabled className="w-full border p-2 rounded" placeholder="Ad Soyad" />
        <input type="text" name="studentNumber" value={form.studentNumber} disabled className="w-full border p-2 rounded" placeholder="Öğrenci No" />
        <input type="email" name="email" value={form.email} disabled className="w-full border p-2 rounded" placeholder="Email" />
        <input type="text" name="phone" value={form.phone} onChange={handleChange} className="w-full border p-2 rounded" placeholder="Telefon" />
        <select name="incomeStatus" value={form.incomeStatus} onChange={handleChange} className="w-full border p-2 rounded">
          <option value="">Ailenin Gelir Durumu</option>
          <option value="Düşük">Düşük</option>
          <option value="Orta">Orta</option>
          <option value="Yüksek">Yüksek</option>
        </select>
        <input type="number" name="siblingCount" value={form.siblingCount} onChange={handleChange} className="w-full border p-2 rounded" placeholder="Kardeş Sayısı" />
        <textarea name="note" value={form.note} onChange={handleChange} className="w-full border p-2 rounded" placeholder="Açıklama (isteğe bağlı)" rows={4} />

        {/* Notların Gösterimi */}
        <div className="border-t pt-4">
          <h2 className="text-lg font-semibold mb-2">📚 Notlar</h2>
          <ul className="space-y-2 text-sm text-gray-700">
            {form.grades.map((grade, idx) => (
              <li key={idx} className="flex justify-between border rounded px-3 py-1">
                <span>{grade.courseName}</span>
                <span>Vize: {grade.midterm} | Final: {grade.final}</span>
              </li>
            ))}
          </ul>
        </div>

        <button type="submit" disabled={submitting} className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700">
          {submitting ? 'Gönderiliyor...' : 'Gönder'}
        </button>
      </form>
    </div>
  );
};

export default ScholarshipApplicationPage;
