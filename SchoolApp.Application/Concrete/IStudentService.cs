using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IStudentService : IGenericService<Student>
{
    Task<IServiceResultWithData<IEnumerable<Student>>> GetStudentsWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Student>> GetStudentByIdWithIncludesAsync(int id, QueryParameters param);
    Task<IServiceResultWithData<Student>> GetMyInfo(int studentId, QueryParameters param);

}