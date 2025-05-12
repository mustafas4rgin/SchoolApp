using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface ITeacherRepository : IGenericRepository
{
    IQueryable<Course> GetTeachersCourses(int teacherId);
}