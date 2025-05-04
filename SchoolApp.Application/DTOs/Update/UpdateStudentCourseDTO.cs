namespace SchoolApp.Application.DTOs.Update;

public class UpdateStudentCourseDTO
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int Attendance { get; set; } = 0;
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}