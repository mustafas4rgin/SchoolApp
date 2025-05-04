namespace SchoolApp.Application.DTOs.Listing;

public class GradeDTO
{
    public int Id { get; set; }
    public int Note { get; set; }
    public string StudentName {get; set; } = null!;
    public string CourseName { get; set; } = null!;
}