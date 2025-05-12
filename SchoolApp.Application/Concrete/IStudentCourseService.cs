using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IStudentCourseService : IGenericService<StudentCourse>
{
    Task<IServiceResult> SelectCoursesAsync(int studentId, List<int> courseIds);
    Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<StudentCourse>> GetStudentCourseByIdWithIncludesAsync(int id, QueryParameters param);
    Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesByStudentsCurrentYear(int studentId, QueryParameters param);
    Task<IServiceResult> ConfirmSelectionAsync(int studentId);
    Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetUnconfirmedSelectionsAsync(int studentId,QueryParameters param);

}