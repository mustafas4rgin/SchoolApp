namespace SchoolApp.Application.DTOs.Create;

public class CreateSurveyQuestionDTO
{
    public string QuestionText { get; set; } = string.Empty;
    public int SurveyId { get; set; }
}