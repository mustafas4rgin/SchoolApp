using SchoolApp.Domain.Entities;

namespace SchoolApp.Domain.Contracts;

public interface IGenericRepository
{
    IQueryable<T> GetAll<T>() where T : EntityBase;
    Task<T?> GetByIdAsync<T>(int id) where T : EntityBase;
    Task<T?> UpdateAsync<T>(T entity) where T : EntityBase;
    Task<T?> Add<T>(T entity) where T : EntityBase;
    void Delete<T>(T entity) where T : EntityBase;
    void SoftDelete<T>(T entity) where T : EntityBase;
    void Restore<T>(T entity) where T : EntityBase;
    void DeleteEntities<T>(IQueryable<T> query) where T : EntityBase;
    Task AddEntitiesAsync<T>(IEnumerable<T> query) where T : EntityBase;
    void UpdateEntities<T>(IEnumerable<T> values) where T : EntityBase;
    Task SaveChangesAsync();
}