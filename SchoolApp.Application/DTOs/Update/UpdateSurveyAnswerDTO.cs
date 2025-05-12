namespace SchoolApp.Application.DTOs.Update;

public class UpdateSurveyAnswerDTO
{
    public int StudentId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
}