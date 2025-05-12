namespace SchoolApp.Domain.Entities;

public class StudentCourse : EntityBase
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int Attendance { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    //navigation properties
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public bool IsConfirmed { get; set; } = false;
}