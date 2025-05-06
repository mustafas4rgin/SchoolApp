namespace SchoolApp.Application.DTOs.Update;

public class UpdateStudentDTO
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] Hash { get; set; } = null!;
    public byte[] Salt { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int Year { get; set; }
    public int RoleId { get; set; }
    public int DepartmentId { get; set; }

}