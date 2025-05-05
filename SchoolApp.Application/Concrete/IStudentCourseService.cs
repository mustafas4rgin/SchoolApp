using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface IStudentCourseService : IGenericService<StudentCourse>
{
    Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<StudentCourse>> GetStudentCourseByIdWithIncludesAsync(int id, QueryParameters param);
    
}