namespace SchoolApp.Domain.Entities;

public class SurveyOption : EntityBase
{
    public string Text { get; set; } = null!;
    public int QuestionId { get; set; }
    public SurveyQuestion Question { get; set; } = null!;
}