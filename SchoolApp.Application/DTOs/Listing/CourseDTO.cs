namespace SchoolApp.Application.DTOs.Listin;

public class CourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int TeacherId { get; set; }
    //navigation properties
    public string TeacherName { get; set; } = null!;
}