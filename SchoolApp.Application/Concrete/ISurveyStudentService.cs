using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface ISurveyStudentService
{
    Task<IServiceResult> HasStudentAnsweredSurvey(int studentId, int surveyId);
    Task<IServiceResult> MarkAsAnswered(int studentId, int surveyId);
}