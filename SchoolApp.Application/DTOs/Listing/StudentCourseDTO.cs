namespace SchoolApp.Application.DTOs.Listing;

public class StudentCourseDTO
{
    public int Id { get; set; }
    public int Attendance { get; set; }
    public DateTime JoinDate { get; set; }
    //navigation properties
    public string StudentName { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public bool IsConfirmed { get; set; } = false;

}