namespace SchoolApp.Domain.Entities;

public class Course : EntityBase
{
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public int TeacherId { get; set; }
    public int DepartmentId { get; set; }
    //navigation properties
    public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;
    public Department Department { get; set; } = null!;
}