namespace SchoolApp.Domain.Entities;

public class EntityBase
{
    public int Id { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}