'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import axiosInstance from '../../../../../../lib/axiosInstance';

interface SurveyOption {
    id: number;
    text: string;
}

interface SurveyQuestion {
    id: number;
    questionText: string;
    options: SurveyOption[];
}

interface SurveyAnswer {
    questionId: number;
    selectedOptionId: number;
    studentId: number;
}

const SurveyDetailPage = () => {
    const { id } = useParams();
    const router = useRouter();
    const [questions, setQuestions] = useState<SurveyQuestion[]>([]);
    const [answers, setAnswers] = useState<Record<number, number>>({});
    const [studentId, setStudentId] = useState<number | null>(null);
    const [loading, setLoading] = useState(true);
    const [submitting, setSubmitting] = useState(false);
    const [alreadyAnswered, setAlreadyAnswered] = useState(false);

    useEffect(() => {
        const fetchSurvey = async () => {
            try {
                const studentRes = await axiosInstance.get('/Student/MyInfo');
                const studentId = studentRes.data.id;
                setStudentId(studentId);

                const checkRes = await axiosInstance.get(
                    `/SurveyStudent/HasAnswered/${id}`
                );

                if (checkRes.data.hasAnswered) {
                    setAlreadyAnswered(true);
                    return;
                }

                const questionsRes = await axiosInstance.get(
                    `/SurveyQuestion/GetAll/${id}?Include=options`
                );
                setQuestions(questionsRes.data);
            } catch (err) {
                console.error('Veriler alınamadı:', err);
            } finally {
                setLoading(false);
            }
        };

        if (id) {
            fetchSurvey();
        }
    }, [id]);

    const handleOptionChange = (questionId: number, optionId: number) => {
        setAnswers(prev => ({ ...prev, [questionId]: optionId }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
    
        if (!studentId) return;
    
        const unansweredQuestions = questions.filter(q => answers[q.id] === undefined);
    
        if (unansweredQuestions.length > 0) {
            alert('Lütfen tüm soruları cevaplayın.');
            return;
        }
    
        const payload: SurveyAnswer[] = Object.entries(answers).map(([qId, oId]) => ({
            questionId: parseInt(qId),
            selectedOptionId: oId,
            studentId: studentId
        }));
    
        try {
            setSubmitting(true);
    
            for (const answer of payload) {
                await axiosInstance.post('/SurveyAnswer/Add', answer);
            }
    
            await axiosInstance.put(`/SurveyStudent/MarkAsAnswered/${id}`);
    
            alert('🎉 Anket başarıyla gönderildi!');
            router.push('/dashboard');
        } catch (err) {
            console.error('Gönderme hatası:', err);
            alert('❌ Anket gönderilirken hata oluştu.');
        } finally {
            setSubmitting(false);
        }
    };
    

    const normalize = (text: string) =>
        text.trim().toLowerCase().replace(/\.$/, '');

    const sortAndDeduplicateOptions = (options: SurveyOption[]) => {
        const order = [
            "kesinlikle katılmıyorum",
            "katılmıyorum",
            "ne katılıyorum ne de katılmıyorum",
            "katılıyorum",
            "kesinlikle katılıyorum"
        ];

        const sorted = [...options].sort((a, b) => {
            const aIndex = order.indexOf(normalize(a.text));
            const bIndex = order.indexOf(normalize(b.text));
            return aIndex - bIndex;
        });

        const seen = new Set<string>();
        return sorted.filter(opt => {
            const norm = normalize(opt.text);
            if (seen.has(norm)) return false;
            seen.add(norm);
            return true;
        });
    };

    if (loading) return <p className="p-8 text-gray-500">Yükleniyor...</p>;

    if (alreadyAnswered) {
        return (
            <div className="p-8 text-center text-gray-700">
                <h2 className="text-xl font-semibold mb-4">Bu anketi zaten cevapladınız.</h2>
                <p className="text-gray-500">Her anket yalnızca bir kez cevaplanabilir.</p>
            </div>
        );
    }

    return (
        <div className="p-8 max-w-3xl mx-auto">
            <h1 className="text-2xl font-bold text-gray-800 mb-6">📝 Anket</h1>
            <form onSubmit={handleSubmit} className="space-y-6 bg-white p-6 rounded-2xl shadow border border-gray-200">
                {questions.map((q) => {
                    const options = sortAndDeduplicateOptions(q.options);

                    return (
                        <div key={q.id} className="space-y-2">
                            <p className="font-medium text-gray-800">{q.questionText}</p>
                            <div className="space-y-1">
                                {options.map((opt) => (
                                    <label key={opt.id} className="flex items-center space-x-2">
                                        <input
                                            type="radio"
                                            name={`question-${q.id}`}
                                            checked={answers[q.id] === opt.id}
                                            onChange={() => handleOptionChange(q.id, opt.id)}
                                        />
                                        <span>{opt.text}</span>
                                    </label>
                                ))}
                            </div>
                        </div>
                    );
                })}
                <button
                    type="submit"
                    disabled={submitting}
                    className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
                >
                    {submitting ? 'Gönderiliyor...' : 'Anketi Gönder'}
                </button>
            </form>
        </div>
    );
};

export default SurveyDetailPage;
