namespace SchoolApp.Application.DTOs.Listing;

public class TeacherDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
}