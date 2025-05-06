namespace SchoolApp.Application.DTOs.Update;

public class UpdateCourseDTO
{
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int TeacherId { get; set; }
    public int DepartmentId { get; set; }
}