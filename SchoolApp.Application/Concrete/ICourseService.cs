using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface ICourseService : IGenericService<Course>
{
    Task<IServiceResultWithData<IEnumerable<Course>>> GetCoursesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Course>> GetCourseByIdWithIncludesAsync(QueryParameters param, int id);
    Task<IServiceResultWithData<IEnumerable<Course>>> AvailableCoursesAsync(QueryParameters param);
    Task<IServiceResult> CloseCourseAsync(Course course);
    Task<IServiceResult> OpenCourseAsync(Course course);
}