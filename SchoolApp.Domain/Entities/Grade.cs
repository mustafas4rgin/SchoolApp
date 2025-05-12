namespace SchoolApp.Domain.Entities;

public class Grade : EntityBase
{
    public int Midterm { get; set; }
    public int Final { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    //navigation properties
    public Course Course { get; set; } = null!;
    public Student Student { get; set; } = null!;
}