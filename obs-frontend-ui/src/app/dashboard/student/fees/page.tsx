'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';
import Link from 'next/link';

interface TuitionPayment {
    term: string;
    totalAmount: number;
    paidAmount: number;
    remainingAmount: number;
    status: string;
    lastPaymentDate: string;
}

const TuitionPage = () => {
    const [payments, setPayments] = useState<TuitionPayment[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchPayments = async () => {
            try {
                const res = await axiosInstance.get('/Tuition/MyPayments');
                setPayments(res.data);
            } catch (err) {
                console.error('Hata:', err);
                setError('Veriler alınamadı.');
            } finally {
                setLoading(false);
            }
        };

        fetchPayments();
    }, []);

    if (loading) return <p className="p-8 text-gray-500">Yükleniyor...</p>;
    if (error) return <p className="p-8 text-red-500">{error}</p>;

    return (
        <div className="p-8 max-w-5xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">💳 Eğitim Ücreti İşlemleri</h1>
            <div className="overflow-x-auto rounded-xl shadow border border-gray-200 bg-white">
                <table className="w-full text-left table-auto">
                    <thead className="bg-gray-100">
                        <tr>
                            <th className="px-4 py-2">Dönem</th>
                            <th className="px-4 py-2">Toplam Ücret</th>
                            <th className="px-4 py-2">Ödenen</th>
                            <th className="px-4 py-2">Kalan</th>
                            <th className="px-4 py-2">Durum</th>
                            <th className="px-4 py-2">Son Ödeme Tarihi</th>
                            <th className="px-4 py-2">İşlem</th>
                        </tr>
                    </thead>
                    <tbody>
                        {payments.map((p, i) => (
                            <tr key={i} className="border-t">
                                <td className="px-4 py-2">{p.term}</td>
                                <td className="px-4 py-2">{p.totalAmount.toLocaleString('tr-TR')} ₺</td>
                                <td className="px-4 py-2">{p.paidAmount.toLocaleString('tr-TR')} ₺</td>
                                <td className="px-4 py-2">{p.remainingAmount.toLocaleString('tr-TR')} ₺</td>
                                <td className="px-4 py-2">
                                    {p.status === 'Tamamlandı' ? (
                                        <span className="text-green-600 font-semibold">Tamamlandı</span>
                                    ) : (
                                        <span className="text-orange-500 font-semibold">Eksik</span>
                                    )}
                                </td>
                                <td className="px-4 py-2">{new Date(p.lastPaymentDate).toLocaleDateString('tr-TR')}</td>
                                <td className="px-4 py-2">
                                    <Link href={`/dashboard/student/tuition?termType=Guz&year=2025`}>
                                        Detay
                                    </Link>


                                </td>

                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default TuitionPage;
