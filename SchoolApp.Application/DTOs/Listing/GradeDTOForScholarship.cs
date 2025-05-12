namespace SchoolApp.Application.DTOs.Listing;

public class GradeDTOForScholarship
{
    public int Midterm { get; set; }
    public int Final { get; set; }
    public string CourseName { get; set; } = string.Empty;
}