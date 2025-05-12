using SchoolApp.Application.DTOs;
using SchoolApp.Application.Services;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface ISurveyAnswerService : IGenericService<SurveyAnswer>
{
    Task<IServiceResultWithData<IEnumerable<SurveyAnswer>>> GetSurveyAnswersWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<SurveyAnswer>> GetSurveyAnswerByIdWithIncludesAsync(int id,QueryParameters param);
}