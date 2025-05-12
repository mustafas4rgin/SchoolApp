using SchoolApp.Application.Concrete;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;

namespace SchoolApp.Application.Services;

public class SurveyStudentService : ISurveyStudentService
{
    private readonly ISurveyStudentRepository _surveyStudentRepository;
    public SurveyStudentService(ISurveyStudentRepository surveyStudentRepository)
    {
        _surveyStudentRepository = surveyStudentRepository;
    }
    public async Task<IServiceResult> HasStudentAnsweredSurvey(int studentId, int surveyId)
    {
        var exists = await _surveyStudentRepository.IsThereSurveyStudent(surveyId, studentId);

        if (exists)
            return new SuccessResult("Student already answered survey.");

        return new ErrorResult("Student didn't answer the survey yet or there is no survey with this id.");
    }
    public async Task<IServiceResult> MarkAsAnswered(int studentId, int surveyId)
    {
        try
        {
            var exists = await _surveyStudentRepository.IsThereSurveyStudent(surveyId, studentId);

            if (exists)
                return new ErrorResult("Student already answered survey.");

            await _surveyStudentRepository.Add(
                new SurveyStudent
                {
                    StudentId = studentId,
                    SurveyId = surveyId,
                    HasAnswered = true
                }
            );
            await _surveyStudentRepository.SaveChangesAsync();

            return new SuccessResult("Marked as answered.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }

    }
}