
namespace SchoolApp.Application.DTOs.Listing;

public class RoleDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<UserDTO> Users { get; set; } = null!;
}