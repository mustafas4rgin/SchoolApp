namespace SchoolApp.Domain.Entities;

public class Department : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int FacultyId { get; set; }
    //navigation properties
    public Faculty Faculty { get; set; } = null!;
    public ICollection<Teacher> Teachers { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = null!;
    public ICollection<Course> Courses { get; set; } = null!;
}