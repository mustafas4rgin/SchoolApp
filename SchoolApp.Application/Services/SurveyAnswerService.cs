using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class SurveyAnswerService : GenericService<SurveyAnswer>, ISurveyAnswerService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IValidator<SurveyAnswer> _validator;
    public SurveyAnswerService(
        IGenericRepository genericRepository,
        IValidator<SurveyAnswer> validator
    ) : base(genericRepository, validator)
    {
        _genericRepository = genericRepository;
        _validator = validator;
    }
    public override async Task<IServiceResult> AddAsync(SurveyAnswer answer)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(answer);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existingAnswer = await _genericRepository.GetAll<SurveyAnswer>()
                                .Include(a => a.Question)
                                .FirstOrDefaultAsync(a => a.StudentId == answer.StudentId && a.QuestionId == answer.QuestionId);

            if (existingAnswer is not null)
                return new ErrorResult("Already answered for this question.");

            await _genericRepository.Add(answer);
            var surveyStudentExists = await _genericRepository.GetAll<SurveyStudent>()
                    .AnyAsync(ss => ss.StudentId == answer.StudentId && ss.SurveyId == answer.Question.SurveyId);

            if (!surveyStudentExists)
            {
                await _genericRepository.Add(new SurveyStudent
                {
                    HasAnswered = true,
                    StudentId = answer.StudentId,
                    SurveyId = answer.Question.SurveyId
                });
            }
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Answered successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<SurveyAnswer>>> GetSurveyAnswersWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<SurveyAnswer>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForSurveyAnswer(query, param.Include);

            var answers = await query.Where(sa => !sa.IsDeleted)
                            .ToListAsync();

            if (!answers.Any())
                return new ErrorResultWithData<IEnumerable<SurveyAnswer>>("There is no answer.");

            return new SuccessResultWithData<IEnumerable<SurveyAnswer>>("Answers: ", answers);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<SurveyAnswer>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<SurveyAnswer>> GetSurveyAnswerByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<SurveyAnswer>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForSurveyAnswer(query, param.Include);

            var answer = await query.FirstOrDefaultAsync(sa => sa.Id == id);

            if (answer is null || answer.IsDeleted)
                return new ErrorResultWithData<SurveyAnswer>($"There is no answer with ID : {id}");

            return new SuccessResultWithData<SurveyAnswer>("Survey answer found.", answer);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<SurveyAnswer>(ex.Message);
        }
    }
}