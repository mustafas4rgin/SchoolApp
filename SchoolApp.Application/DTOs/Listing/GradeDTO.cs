namespace SchoolApp.Application.DTOs.Listing;

public class GradeDTO
{
    public int Id { get; set; }
    public int Midterm { get; set; }
    public int Final { get; set; }
    public string StudentName {get; set; } = null!;
    public string CourseName { get; set; } = null!;
}