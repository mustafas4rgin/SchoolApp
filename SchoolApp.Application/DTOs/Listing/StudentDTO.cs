namespace SchoolApp.Application.DTOs.Listing;

public class StudentDTO
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string RoleName { get; set; } = null!;
    public List<GradesForStudentDTO> Grades { get; set; } = null!;
    public List<CoursesForUserDTO> StudentCourses { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;

}