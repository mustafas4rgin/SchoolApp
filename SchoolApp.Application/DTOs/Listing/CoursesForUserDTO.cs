namespace SchoolApp.Application.DTOs.Listing;

public class CoursesForUserDTO
{
    public int Id { get; set; }
    public int Attendance { get; set; }
    public DateTime JoinDate { get; set; }
    //navigation properties
    public string CourseName { get; set; } = null!;
}