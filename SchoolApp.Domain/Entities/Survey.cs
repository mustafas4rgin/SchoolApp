namespace SchoolApp.Domain.Entities;

public class Survey : EntityBase
{
    public string Title { get; set; } = string.Empty;
    //navigation properties
    public ICollection<SurveyQuestion> Questions { get; set; } = null!;
    public ICollection<SurveyStudent> AnsweredStudents { get; set; } = null!;
}