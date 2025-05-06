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

public class CourseService : GenericService<Course>, ICourseService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IValidator<Course> _validator;
    public CourseService(
        IGenericRepository genericRepository,
        IValidator<Course> validator
    ) : base(genericRepository, validator)
    {
        _genericRepository = genericRepository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<Course>> GetCourseByIdWithIncludesAsync(QueryParameters param, int id)
    {
        try
        {
            var query = _genericRepository.GetAll<Course>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForCourse(query,param.Include);

            var course = await query.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (course is null)
                return new ErrorResultWithData<Course>($"There is no course with ID : {id}");

            return new SuccessResultWithData<Course>("Course found.",course);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Course>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<Course>>> GetCoursesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Course>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForCourse(query,param.Include);

            var courses = await query.Where(c => !c.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.Name.ToLower().Contains(param.Search.ToLower()))
                            .ToListAsync();
            
            if (!courses.Any())
                return new ErrorResultWithData<IEnumerable<Course>>("There is no course.");

            return new SuccessResultWithData<IEnumerable<Course>>("Courses found.",courses);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Course>>(ex.Message);
        }
    }
    public override async Task<IServiceResult> UpdateAsync(Course course)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(course);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            
            var teacher = await _genericRepository.GetByIdAsync<Teacher>(course.TeacherId);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResult($"There is no teacher with ID : {course.TeacherId}");
            
            var department = await _genericRepository.GetByIdAsync<Department>(course.DepartmentId);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {course.DepartmentId}");

            var existingCourse = await _genericRepository.GetAll<Course>()
                                    .FirstOrDefaultAsync(c => c.Name == course.Name && !c.IsDeleted);
            
            if (existingCourse is not null)
                return new ErrorResult("This course is already registered.");

            await _genericRepository.UpdateAsync(course);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Course updated successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(Course course)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(course);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));
            
            var teacher = await _genericRepository.GetByIdAsync<Teacher>(course.TeacherId);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResult($"There is no teacher with ID : {course.TeacherId}");

            var department = await _genericRepository.GetByIdAsync<Department>(course.DepartmentId);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {course.DepartmentId}");

            var existingCourse = await _genericRepository.GetAll<Course>()
                                    .FirstOrDefaultAsync(c => c.Name == course.Name && !c.IsDeleted);

            if (existingCourse is not null)
                return new ErrorResult($"This course is already registered.");
            
            await _genericRepository.Add(course);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Course added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }

}