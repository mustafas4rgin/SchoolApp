using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface ISurveyQuestionRepository : IGenericRepository
{
    IQueryable<SurveyQuestion> GetAllBySurveyId(int surveyId);
}