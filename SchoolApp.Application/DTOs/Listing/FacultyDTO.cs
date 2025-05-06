using SchoolApp.Application.DTOs.Listin;

namespace SchoolApp.Application.DTOs.Listing;

public class FacultyDTO
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //navigation properties
    public List<CourseDTO> Courses { get; set; } = null!;
    public List<DepartmentDTO> Departments { get; set; } = null!;
}