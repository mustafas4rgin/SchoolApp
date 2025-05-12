namespace SchoolApp.Application.DTOs.Create;

public class CreateSurveyAnswerDTO
{
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    public int StudentId { get; set; }
}
