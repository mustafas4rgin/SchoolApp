namespace SchoolApp.Application.DTOs.Listing;

public class CourseDTOForTeacher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Year { get; set; }
    public string DepartmentName { get; set; } = null!;
    public bool IsAvailable { get; set; }
    public int Credit { get; set; }

}