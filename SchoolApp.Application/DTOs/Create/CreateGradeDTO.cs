namespace SchoolApp.Application.DTOs.Create;

public class CreateGradeDTO
{
    public int Midterm { get; set; }
    public int Final { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }

}