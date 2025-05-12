using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Concrete;

public interface IFacultyService : IGenericService<Faculty>
{
    Task<IServiceResultWithData<IEnumerable<Faculty>>> GetFacultiesWithIncludesAsync(QueryParameters param);
    Task<IServiceResultWithData<Faculty>> GetFacultyByIdWithIncludesAsync(int id, QueryParameters param);
}