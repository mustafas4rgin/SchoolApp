namespace SchoolApp.Domain.Entities;

public class Teacher : User
{
    public string Number { get; set; } = null!;
    public int DepartmentId { get; set; }
    //navigation properties
    public ICollection<Course> Courses { get; set; } = null!;
    public Department Department { get; set; } = null!;
}