namespace SchoolApp.Domain.Entities;

public class SurveyStudent : EntityBase
{
    public int StudentId { get; set; }
    public int SurveyId { get; set; }
    public bool HasAnswered { get; set; } = false;
    //navigation properties
    public Student Student { get; set; } = null!;
    public Survey Survey { get; set; } = null!;
}