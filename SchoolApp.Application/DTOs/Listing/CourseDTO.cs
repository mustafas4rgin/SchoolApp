using SchoolApp.Application.DTOs.Listing;

namespace SchoolApp.Application.DTOs.Listin;

public class CourseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    //navigation properties
    public string TeacherName { get; set; } = null!;
    public string DepartmentName { get; set; } = null!;
    public List<GradeDTOForCourse> Grades {get; set; } = null!;
    public List<string> StudentNames { get; set; } = null!;
}