using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class SurveyQuestionService : GenericService<SurveyQuestion>, ISurveyQuestionService
{
    private readonly ISurveyQuestionRepository _surveyQuestionRepository;
    public SurveyQuestionService(
        IValidator<SurveyQuestion> validator,
        ISurveyQuestionRepository surveyQuestionRepository
    ) : base(surveyQuestionRepository,validator)
    {
        _surveyQuestionRepository = surveyQuestionRepository;
    }
    public async Task<IServiceResultWithData<IEnumerable<SurveyQuestion>>> GetSurveyQuestionsBySurveyIdWithIncludesAsync(int surveyId, QueryParameters param)
    {
        try
        {
            var query = _surveyQuestionRepository.GetAllBySurveyId(surveyId);

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForSurveyQuestion(query, param.Include);

            var questions = await query.Where(sq => !sq.IsDeleted)
                            .ToListAsync();

            if (!questions.Any())
                return new ErrorResultWithData<IEnumerable<SurveyQuestion>>("No question found.");

            return new SuccessResultWithData<IEnumerable<SurveyQuestion>>("Questions found.",questions);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<SurveyQuestion>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<SurveyQuestion>>> GetSurveyQuestionsWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _surveyQuestionRepository.GetAll<SurveyQuestion>();

            if(!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForSurveyQuestion(query,param.Include);

            var questions = await query.Where(sq => !sq.IsDeleted)
                                .ToListAsync();

            if (!questions.Any())
                return new ErrorResultWithData<IEnumerable<SurveyQuestion>>("There is no question.");

            return new SuccessResultWithData<IEnumerable<SurveyQuestion>>("Questions found.",questions);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<SurveyQuestion>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<SurveyQuestion>> GetSurveyQuestionByIdWithIncludesAync(int id, QueryParameters param)
    {
        try
        {
            var query = _surveyQuestionRepository.GetAll<SurveyQuestion>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForSurveyQuestion(query,param.Include);

            var question = await query.Where(sq => !sq.IsDeleted)
                                .FirstOrDefaultAsync(sq => sq.Id == id);

            if (question is null)
                return new ErrorResultWithData<SurveyQuestion>($"There is no question with ID : {id}");

            return new SuccessResultWithData<SurveyQuestion>("Question found.",question);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<SurveyQuestion>(ex.Message);
        }
    }
}