namespace SchoolApp.Application.DTOs.Update;

public class UpdateDepartmentDTO
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int FacultyId { get; set; }
}