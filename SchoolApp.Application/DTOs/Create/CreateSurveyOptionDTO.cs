namespace SchoolApp.Application.DTOs;

public class CreateSurveyOptionDTO
{
    public string Text { get; set; } = null!;
    public int QuestionId { get; set; }
}