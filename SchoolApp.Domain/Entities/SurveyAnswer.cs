namespace SchoolApp.Domain.Entities;

public class SurveyAnswer : EntityBase
{
    public int StudentId { get; set; }
    public int QuestionId { get; set; }
    public int SelectedOptionId { get; set; }
    //navigation properties
    public SurveyQuestion Question { get; set; } = null!;
    public SurveyOption Option { get; set; } = null!;
}