using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface ICourseService : IGenericService<Course>
{
    Task<IServiceResultWithData<IEnumerable<Course>>> GetCoursesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Course>> GetCourseByIdWithIncludesAsync(QueryParameters param, int id);
}