namespace SchoolApp.Application.DTOs.Listing;

public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string RoleName { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
}