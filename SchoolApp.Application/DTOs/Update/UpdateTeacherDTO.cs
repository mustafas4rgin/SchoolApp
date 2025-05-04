namespace SchoolApp.Application.DTOs;

public class UpdateTeacherDTO
{
    public string Name { get; set; } = null!;
    public int RoleId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] Hash { get; set; } = null!;
    public byte[] Salt { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}