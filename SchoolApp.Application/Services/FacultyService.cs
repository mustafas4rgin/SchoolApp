using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class FacultyService : GenericService<Faculty>, IFacultyService
{
    private readonly IGenericRepository _genericRepository;
    public FacultyService(
        IGenericRepository repository,
        IValidator<Faculty> validator
    ) : base(repository,validator)
    {
        _genericRepository = repository;
    }
    public async Task<IServiceResultWithData<IEnumerable<Faculty>>> GetFacultiesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Faculty>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForFaculty(query,param.Include);

            var faculties = await query.Where(f => !f.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Title.ToLower().Contains(param.Search.ToLower()))
                            .ToListAsync();

            if (!faculties.Any())
                return new ErrorResultWithData<IEnumerable<Faculty>>("There is no faculty.");

            return new SuccessResultWithData<IEnumerable<Faculty>>("Faculties found.",faculties);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Faculty>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Faculty>> GetFacultyByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Faculty>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForFaculty(query,param.Include);

            var faculty = await query
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Title.ToLower().Contains(param.Search.ToLower()))
                            .FirstOrDefaultAsync(f => f.Id == id);

            if (faculty is null || faculty.IsDeleted)
                return new ErrorResultWithData<Faculty>($"There is no faculty with ID : {id}");

            return new SuccessResultWithData<Faculty>("Faculty found.",faculty);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Faculty>(ex.Message);
        }
    }
    public override Task<IServiceResultWithData<IEnumerable<Faculty>>> GetAllAsync()
    {
        throw new NotSupportedException("Use GetFacultiesWithIncludesAsync instead.");
    }

}