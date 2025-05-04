namespace SchoolApp.Domain.Entities;

public class Role : EntityBase
{
    public string Name { get; set; } = null!;
    //navigation properties
    public ICollection<User> Users { get; set; } = null!;
}