namespace SchoolApp.Application.DTOs.Listing;

public class DepartmentDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int FacultyId { get; set; }
    //navigation properties
    public string FacultyName { get; set; } = null!;
    public List<TeacherDTO> Teachers { get; set; } = null!;
    public List<StudentDTO>  Students { get; set; } = null!;
}