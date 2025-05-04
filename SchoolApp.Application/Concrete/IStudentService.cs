using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface IStudentService : IGenericService<Student>
{
    Task<IServiceResultWithData<IEnumerable<Student>>> GetStudentsWithIncludesAsync(QueryParameters param);
}