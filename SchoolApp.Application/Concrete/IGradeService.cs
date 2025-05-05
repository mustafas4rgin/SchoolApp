using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IGradeService : IGenericService<Grade>
{
    Task<IServiceResultWithData<IEnumerable<Grade>>> GetGradesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Grade>> GetGradeByIdWithIncludesAsync(QueryParameters param, int id);
    Task<IServiceResultWithData<IEnumerable<Grade>>> GetStudentsGradesAsync(int studentId, QueryParameters param);
    Task<IServiceResultWithData<IEnumerable<Grade>>> GetGradesByCourseIdAsync(int courseId, QueryParameters param);
}