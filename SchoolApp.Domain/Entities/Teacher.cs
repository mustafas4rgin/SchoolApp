namespace SchoolApp.Domain.Entities;

public class Teacher : User
{
    public string Number { get; set; } = null!;
    //navigation properties
    public ICollection<Course> Courses { get; set; } = null!;
}