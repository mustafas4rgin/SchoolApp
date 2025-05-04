using System.Net.NetworkInformation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class StudentService :GenericService<Student>, IStudentService
{
    private readonly IGenericRepository _genericRepository;
    public StudentService(
        IGenericRepository repository,
        IValidator<Student> validator
    ) : base(repository, validator) 
    {
        _genericRepository = repository;
    }
    public async Task<IServiceResultWithData<IEnumerable<Student>>> GetStudentsWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Student>();

            if (!string.IsNullOrEmpty(param.Include))
            {
                var includes = param.Include.Split(',',StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var inc in includes.Select(i => i.Trim().ToLower()))
                {
                    if (inc == "all")
                        query = query.Include(s => s.Role)
                                    .Include(s => s.StudentCourses)
                                        .ThenInclude(sc => sc.Course)
                                    .Include(s => s.Grades)
                                        .ThenInclude(g => g.Course);
                    else
                    {
                        if (inc == "grades")
                            query = query.Include(s => s.Grades)
                                .ThenInclude(g => g.Course);
                        else if (inc == "courses")
                            query = query.Include(s => s.StudentCourses)
                                .ThenInclude(sc => sc.Course);
                        else if (inc == "role")
                            query = query.Include(s => s.Role);
                    }
                }
            }
            
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