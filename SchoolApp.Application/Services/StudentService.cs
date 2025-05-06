using System.Net.NetworkInformation;
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

public class StudentService :GenericService<Student>, IStudentService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IValidator<Student> _validator;
    public StudentService(
        IGenericRepository repository,
        IValidator<Student> validator
    ) : base(repository, validator) 
    {
        _genericRepository = repository;
        _validator = validator;
    }
    public override async Task<IServiceResult> UpdateAsync(Student student)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(student);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
                
            var department = await _genericRepository.GetByIdAsync<Department>(student.Id);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {student.DepartmentId}");

            await _genericRepository.UpdateAsync(student);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Student added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(Student student)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(student);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
                
            var department = await _genericRepository.GetByIdAsync<Department>(student.DepartmentId);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {student.DepartmentId}");

            await _genericRepository.Add(student);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Student added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Student>> GetStudentByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Student>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudent(query, param.Include);

            var student = await query
                    .Where(p => string.IsNullOrEmpty(param.Search) || p.FirstName.ToLower().Contains(param.Search.ToLower()))
                    .FirstOrDefaultAsync(s => s.Id == id);

            if (student is null || student.IsDeleted)
                return new ErrorResultWithData<Student>($"There is no student with ID : {id}");
            
            return new SuccessResultWithData<Student>("Student found.",student);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Student>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<Student>>> GetStudentsWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Student>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudent(query,param.Include);
            
            var students = await query.Where(s => !s.IsDeleted)
                                .ToListAsync();
            
            if (!students.Any())
                return new ErrorResultWithData<IEnumerable<Student>>("There is no student.");

            return new SuccessResultWithData<IEnumerable<Student>>("Students found.", students);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Student>>(ex.Message);
        }
    }
}