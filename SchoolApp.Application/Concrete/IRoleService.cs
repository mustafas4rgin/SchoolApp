
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface IRoleService : IGenericService<Role>
{
    Task<IServiceResultWithData<IEnumerable<Role>>> GetRolesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Role>> GetRoleByIdWithIncludesAsync(int id, QueryParameters param);
}