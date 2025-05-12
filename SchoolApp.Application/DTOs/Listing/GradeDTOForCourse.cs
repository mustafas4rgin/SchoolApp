namespace SchoolApp.Application.DTOs.Listing;

public class GradeDTOForCourse
{
    public int Id { get; set; }
    public int Midterm { get; set; }
    public int Final { get; set; }
    public string StudentName {get; set; } = null!;
}