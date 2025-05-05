using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class GenericService<T> : IGenericService<T> where T :EntityBase
{
    private readonly IGenericRepository _repository;
    private readonly IValidator<T> _validator;
    public GenericService(IGenericRepository repository, IValidator<T> validator)
    {
        _validator = validator;
        _repository = repository;
    }
    public virtual async Task<IServiceResultWithData<IEnumerable<T>>> GetAllAsync()
    {
        try
        {
            var entities = await _repository.GetAll<T>()
                                .Where(e => !e.IsDeleted)
                                .ToListAsync();

            if (!entities.Any())
                return new ErrorResultWithData<IEnumerable<T>>("There is no entity.");

            return new SuccessResultWithData<IEnumerable<T>>("Entities found.",entities);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<T>>(ex.Message);
        }
    }
    public virtual async Task<IServiceResultWithData<T>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync<T>(id);

            if (entity is null || entity.IsDeleted)
                return new ErrorResultWithData<T>($"There is no entity with ID : {id}");

            return new SuccessResultWithData<T>("Entity found.",entity);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<T>(ex.Message);
        }
    }
    public async Task<IServiceResult> AddAsync(T entity)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            await _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return new SuccessResult("Entity added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public virtual async Task<IServiceResult> UpdateAsync(T entity)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (entity.IsDeleted)
                return new ErrorResult("Entity doesn't exist.");

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return new SuccessResult("Entity updated successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> SoftDeleteAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync<T>(id);
            
            if (entity is null || entity.IsDeleted)
                return new ErrorResult($"There is no entity with ID : {id}.");

            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
                
            _repository.SoftDelete(entity);
            await _repository.SaveChangesAsync();

            return new SuccessResult("Entity deleted successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> DeleteAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync<T>(id);
            
            if (entity is null)
                return new ErrorResult($"There is no entity with ID : {id}.");
                
            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            return new SuccessResult("Entity removed successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> RestoreAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync<T>(id);
            
            if (entity is null || !entity.IsDeleted)
                return new ErrorResult($"There is no entity with ID : {id}.");

            var validationResult = await _validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            
            _repository.Restore(entity);
            await _repository.SaveChangesAsync();

            return new SuccessResult("Entity restored successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<T>>> GetAllDeleted()
    {
        try
        {
            var entities = await _repository.GetAll<T>()
                                .Where(e => e.IsDeleted)
                                .ToListAsync();

            if (!entities.Any())
                return new ErrorResultWithData<IEnumerable<T>>("There is no entity.");

            return new SuccessResultWithData<IEnumerable<T>>("Entities found.",entities);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<T>>(ex.Message);
        }
    }
}