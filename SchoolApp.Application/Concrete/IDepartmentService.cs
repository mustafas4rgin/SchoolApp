using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IDepartmentService : IGenericService<Department>
{
    Task<IServiceResultWithData<IEnumerable<Department>>> GetDepartmentsWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Department>> GetDepartmentByIdWithIncludesAsync(int id, QueryParameters param);
}