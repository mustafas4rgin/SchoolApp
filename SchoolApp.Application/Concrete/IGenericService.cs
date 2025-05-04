using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.Application.Concrete;

public interface IGenericService<T> where T : EntityBase
{
    Task<IServiceResult> RestoreAsync(int id);
    Task<IServiceResultWithData<IEnumerable<T>>> GetAllAsync();
    Task<IServiceResultWithData<T>> GetByIdAsync(int id);
    Task<IServiceResult> AddAsync(T entity);
    Task<IServiceResult> UpdateAsync(T entity);
    Task<IServiceResult> SoftDeleteAsync(int id);
    Task<IServiceResult> DeleteAsync(int id);
    Task<IServiceResultWithData<IEnumerable<T>>> GetAllDeleted();

}