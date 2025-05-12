'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

interface Grade {
  courseName: string;
  midterm: number;
  final: number;
}

const AllGradesPage = () => {
  const [grades, setGrades] = useState<Grade[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axiosInstance
      .get('/Student/MyInfo?Include=all')
      .then((res) => setGrades(res.data.grades))
      .catch((err) => console.error('Notlar alınamadı:', err))
      .finally(() => setLoading(false));
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

  const handleDownloadPDF = () => {
    const doc = new jsPDF();
    doc.setFontSize(16);
    doc.text('Tüm Notlar', 14, 18);

    const tableData = grades.map((grade) => {
      const finalEntered = grade.final > 0;
      const average = finalEntered
        ? (grade.midterm * 0.4 + grade.final * 0.6).toFixed(1)
        : '-';
      const letter = finalEntered ? getLetter(Number(average)) : '-';

      return [
        grade.courseName,
        grade.midterm.toString(),
        finalEntered ? grade.final.toString() : 'Girilmedi',
        average,
        letter,
      ];
    });

    autoTable(doc, {
      head: [['Ders', 'Vize', 'Final', 'Ortalama', 'Harf Notu']],
      body: tableData,
      startY: 24,
    });

    doc.save('notlar.pdf');
  };

  return (
    <div className="p-8">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-gray-800">📋 Tüm Notlar</h1>
        <button
  onClick={handleDownloadPDF}
  className="inline-flex items-center gap-2 px-4 py-2 bg-white border border-gray-300 text-gray-800 text-sm rounded-xl shadow-sm hover:bg-gray-100 hover:border-gray-400 transition"
>
  <svg
    xmlns="http://www.w3.org/2000/svg"
    className="w-4 h-4 text-red-500"
    fill="none"
    viewBox="0 0 24 24"
    stroke="currentColor"
    strokeWidth={2}
  >
    <path
      strokeLinecap="round"
      strokeLinejoin="round"
      d="M12 4v16m8-8H4"
    />
  </svg>
  PDF Olarak İndir
</button>

      </div>

      {loading ? (
        <p className="text-gray-500">Yükleniyor...</p>
      ) : grades.length === 0 ? (
        <p className="text-gray-500">Henüz girilmiş not yok.</p>
      ) : (
        <div className="overflow-x-auto bg-white rounded-2xl shadow p-4 border border-gray-200">
          <table className="w-full text-sm text-gray-700">
            <thead className="bg-gray-100">
              <tr>
                <th className="py-2 px-4 text-left">Ders</th>
                <th className="py-2 px-4 text-left">Vize</th>
                <th className="py-2 px-4 text-left">Final</th>
                <th className="py-2 px-4 text-left">Ortalama</th>
                <th className="py-2 px-4 text-left">Harf Notu</th>
              </tr>
            </thead>
            <tbody>
              {grades.map((grade, i) => {
                const finalEntered = grade.final > 0;
                const average = finalEntered
                  ? (grade.midterm * 0.4 + grade.final * 0.6).toFixed(1)
                  : '-';
                const letter = finalEntered ? getLetter(Number(average)) : '-';

                return (
                  <tr key={i} className="border-t">
                    <td className="py-2 px-4">{grade.courseName}</td>
                    <td className="py-2 px-4">{grade.midterm}</td>
                    <td className="py-2 px-4">
                      {finalEntered ? grade.final : <span className="italic text-gray-500">Girilmedi</span>}
                    </td>
                    <td className="py-2 px-4">{average}</td>
                    <td className="py-2 px-4 font-medium">{letter}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default AllGradesPage;
