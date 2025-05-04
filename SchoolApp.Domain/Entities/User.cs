namespace SchoolApp.Domain.Entities;

public class User : EntityBase
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] Hash { get; set; } = null!;
    public byte[] Salt { get; set; } = null!;
    public string Phone { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}