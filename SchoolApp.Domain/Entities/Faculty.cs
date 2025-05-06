namespace SchoolApp.Domain.Entities;

public class Faculty : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //navigation properties
    public ICollection<Department> Departments { get; set; } = null!;
}