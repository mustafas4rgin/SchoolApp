using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface ISurveyStudentRepository : IGenericRepository
{
    Task<bool> IsThereSurveyStudent(int surveyId, int studentId);
}