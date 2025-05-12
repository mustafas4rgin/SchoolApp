using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface ISurveyQuestionService : IGenericService<SurveyQuestion>
{
    Task<IServiceResultWithData<IEnumerable<SurveyQuestion>>> GetSurveyQuestionsWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<SurveyQuestion>> GetSurveyQuestionByIdWithIncludesAync(int id, QueryParameters param);
    Task<IServiceResultWithData<IEnumerable<SurveyQuestion>>> GetSurveyQuestionsBySurveyIdWithIncludesAsync(int surveyId, QueryParameters param);
}