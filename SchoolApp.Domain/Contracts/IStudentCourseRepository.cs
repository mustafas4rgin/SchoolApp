using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface IStudentCourseRepository : IGenericRepository
{
    IQueryable<StudentCourse> GetCoursesByStudentsCurrentYear(int studentId, int studentYear);
    IQueryable<StudentCourse> GetUnconfirmedSelections(int studentId);
}