namespace SchoolApp.Domain.Entities;

public class SurveyQuestion : EntityBase
{
    public string QuestionText { get; set; } = string.Empty;
    public int SurveyId { get; set; }
    //navigation properties
    public Survey Survey { get; set; } = null!;
    public ICollection<SurveyOption> Options { get; set; } = null!;
}