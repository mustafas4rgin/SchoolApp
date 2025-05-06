namespace SchoolApp.Application.DTOs.Create;

public class CreateDepartmentDTO
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int FacultyId { get; set; }
}