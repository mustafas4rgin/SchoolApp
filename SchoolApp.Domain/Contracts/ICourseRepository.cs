using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface ICourseRepository : IGenericRepository
{
    public IQueryable<Course> AvailableCourses();
}