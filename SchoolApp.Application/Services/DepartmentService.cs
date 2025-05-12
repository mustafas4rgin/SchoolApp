using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.Raw;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class DepartmentService : GenericService<Department>, IDepartmentService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IValidator<Department> _validator;
    public DepartmentService(
        IGenericRepository repository,
        IValidator<Department> validator
    ) : base(repository, validator)
    {
        _genericRepository = repository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<IEnumerable<Department>>> GetDepartmentsWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Department>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForDepartment(query, param.Include);

            var departments = await query.Where(f => !f.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Title.ToLower().Contains(param.Search.ToLower()))
                            .ToListAsync();

            if (!departments.Any())
                return new ErrorResultWithData<IEnumerable<Department>>("There is no department.");

            return new SuccessResultWithData<IEnumerable<Department>>("Departments found.", departments);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Department>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Department>> GetDepartmentByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Department>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForDepartment(query, param.Include);

            var department = await query
                                .Where(p => string.IsNullOrEmpty(param.Search) || p.Title.ToLower().Contains(param.Search.ToLower()))
                                .FirstOrDefaultAsync(d => d.Id == id);

            if (department is null || department.IsDeleted)
                return new ErrorResultWithData<Department>($"There is no department with ID : {id}");

            return new SuccessResultWithData<Department>("Department found.",department);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Department>(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(Department department)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(department);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            
            var faculty = await _genericRepository.GetByIdAsync<Faculty>(department.FacultyId);

            if (faculty is null || faculty.IsDeleted)
                return new ErrorResult($"There is no faculty with ID : {department.FacultyId}");

            await _genericRepository.Add(department);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Department added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public override Task<IServiceResultWithData<IEnumerable<Department>>> GetAllAsync()
    {
        throw new NotSupportedException("Use GetDepartmentsWithIncludesAsync instead.");
    }
}