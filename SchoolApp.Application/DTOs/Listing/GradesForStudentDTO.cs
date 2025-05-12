namespace SchoolApp.Application.DTOs.Listing;

public class GradesForStudentDTO
{
    public int Id { get; set; }
    public int Midterm { get; set; }
    public int Final { get; set; }
    public string CourseName { get; set; } = null!;
}