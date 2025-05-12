'use client';

import { useEffect, useState } from 'react';
import { useSearchParams } from 'next/navigation';
import axiosInstance from '../../../../../lib/axiosInstance';

interface TuitionPayment {
    termType: number; // 1 = Guz, 2 = Bahar, 3 = Yaz
    year: number;
    totalAmount: number;
    paidAmount: number;
    remainingAmount: number;
    status: string;
    lastPaymentDate: string;
}

const termTypeMap: Record<number, string> = {
    1: 'Guz',
    2: 'Bahar',
    3: 'Yaz'
};

const reverseTermTypeMap: Record<string, number> = {
    Guz: 1,
    Bahar: 2,
    Yaz: 3
};

const TuitionDetailPage = () => {
    const searchParams = useSearchParams();
    const termTypeStr = searchParams.get('termType'); // e.g., "Guz"
    const year = Number(searchParams.get('year'));

    const [payment, setPayment] = useState<TuitionPayment | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            if (!termTypeStr || !year) return;

            try {
                const res = await axiosInstance.get('/Tuition/MyPayments');
                const data: TuitionPayment[] = res.data;

                const selected = data.find(
                    p => p.termType === reverseTermTypeMap[termTypeStr] && p.year === year
                );

                if (!selected) {
                    setError('Ödeme bulunamadı.');
                } else {
                    setPayment(selected);
                }
            } catch (err) {
                console.error(err);
                setError('Veri alınırken hata oluştu.');
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [termTypeStr, year]);

    const handleMockPayment = async () => {
        if (!payment) return;

        try {
            await axiosInstance.post('/Tuition/MockPayment', {
                termType: termTypeMap[payment.termType], // ✅ string: "Guz"
                year: payment.year,
                amount: payment.remainingAmount
            });

            alert('✅ Ödeme başarıyla işlendi (mock).');
            window.location.reload();
        } catch (err) {
            console.error(err);
            alert('❌ Ödeme sırasında hata oluştu.');
        }
    };

    if (!termTypeStr || !year) return <p className="p-8 text-red-500">❌ Geçersiz dönem bilgisi.</p>;
    if (loading) return <p className="p-8 text-gray-500">Yükleniyor...</p>;
    if (error) return <p className="p-8 text-red-500">{error}</p>;
    if (!payment) return null;

    return (
        <div className="p-8 max-w-xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">
                📄 {termTypeStr} {year} Dönemi Detayı
            </h1>
            <div className="bg-white rounded-xl shadow p-6 space-y-4 border border-gray-200">
                <p><strong>Toplam Ücret:</strong> {payment.totalAmount.toLocaleString('tr-TR')} ₺</p>
                <p><strong>Ödenen:</strong> {payment.paidAmount.toLocaleString('tr-TR')} ₺</p>
                <p><strong>Kalan:</strong> {payment.remainingAmount.toLocaleString('tr-TR')} ₺</p>
                <p><strong>Son Ödeme Tarihi:</strong> {new Date(payment.lastPaymentDate).toLocaleDateString('tr-TR')}</p>
                <p>
                    <strong>Durum:</strong>{' '}
                    {payment.status === 'Tamamlandı' ? (
                        <span className="text-green-600 font-semibold">Tamamlandı</span>
                    ) : (
                        <span className="text-orange-500 font-semibold">Eksik</span>
                    )}
                </p>

                {payment.remainingAmount > 0 && (
                    <button
                        onClick={handleMockPayment}
                        className="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
                    >
                        💳 Ödeme Yap (Mock)
                    </button>
                )}
            </div>
        </div>
    );
};

export default TuitionDetailPage;
