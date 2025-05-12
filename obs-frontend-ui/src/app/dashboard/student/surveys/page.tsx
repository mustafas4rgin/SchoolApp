'use client';

import { useEffect, useState } from 'react';
import axiosInstance from '../../../../../lib/axiosInstance';
import Link from 'next/link';

interface Survey {
    id: number;
    title: string;
}

const SurveyListPage = () => {
    const [surveys, setSurveys] = useState<Survey[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchSurveys = async () => {
            try {
                const res = await axiosInstance.get('/Survey/GetAll');
                setSurveys(res.data);
            } catch (err) {
                console.error('Anketler alınamadı:', err);
            } finally {
                setLoading(false);
            }
        };

        fetchSurveys();
    }, []);

    if (loading) return <p className="p-8 text-gray-500">Yükleniyor...</p>;

    return (
        <div className="p-8 max-w-3xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">📋 Anketler</h1>
            <ul className="space-y-4">
                {surveys.map((survey) => (
                    <li key={survey.id} className="bg-white p-4 rounded shadow border hover:bg-gray-50">
                        <Link
                            href={`/dashboard/student/surveys/${survey.id}`}
                            className="text-blue-600 font-medium hover:underline"
                        >
                            {survey.title}
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SurveyListPage;
