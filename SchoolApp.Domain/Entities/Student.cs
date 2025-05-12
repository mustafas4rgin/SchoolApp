namespace SchoolApp.Domain.Entities;

public class Student : User
{
    public string Number { get; set; } = string.Empty;
    public int Year { get; set; }
    public int DepartmentId { get; set; }
    //navigation properties
    public ICollection<StudentCourse> StudentCourses {get; set;} = null!;
    public ICollection<SurveyStudent> AnsweredSurveys { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public ICollection<Grade> Grades { get; set; } = null!;
}