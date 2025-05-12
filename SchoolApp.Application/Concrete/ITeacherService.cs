using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface ITeacherService : IGenericService<Teacher>
{
    Task<IServiceResultWithData<IEnumerable<Teacher>>> GetTeachersWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Teacher>> GetTeacherByIdWithIncludesAsync(int id, QueryParameters param);
    Task<IServiceResultWithData<IEnumerable<Course>>> GetTeachersCourses(int teacherId, QueryParameters param);
}