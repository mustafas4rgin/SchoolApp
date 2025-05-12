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
    private readonly ICourseRepository _courseRepository;
    private readonly IValidator<Course> _validator;
    public CourseService(
        ICourseRepository genericRepository,
        IValidator<Course> validator
    ) : base(genericRepository, validator)
    {
        _courseRepository = genericRepository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<IEnumerable<Course>>> AvailableCoursesAsync(QueryParameters param)
    {
        try
        {
            var query = _courseRepository.AvailableCourses();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForCourse(query, param.Include);

            var courses = await query.ToListAsync();

            if (!courses.Any())
                return new ErrorResultWithData<IEnumerable<Course>>("There is no available course.");

            return new SuccessResultWithData<IEnumerable<Course>>("Available courses: ",courses);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Course>>(ex.Message);
        }
    }
    public async Task<IServiceResult> OpenCourseAsync(Course course)
    {
        try
        {
            if (course.IsAvailable)
                return new ErrorResult("Course already open.");

            course.IsAvailable = true;

            await _courseRepository.SaveChangesAsync();
            
            return new SuccessResult("Course opened.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> CloseCourseAsync(Course course)
    {
        try
        {
            if (!course.IsAvailable)
                return new ErrorResult("Coure already closed.");

            course.IsAvailable = false;

            await _courseRepository.SaveChangesAsync();

            return new SuccessResult("Course closed.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Course>> GetCourseByIdWithIncludesAsync(QueryParameters param, int id)
    {
        try
        {
            var query = _courseRepository.GetAll<Course>();

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
            var query = _courseRepository.GetAll<Course>();

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
            
            var teacher = await _courseRepository.GetByIdAsync<Teacher>(course.TeacherId);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResult($"There is no teacher with ID : {course.TeacherId}");
            
            var department = await _courseRepository.GetByIdAsync<Department>(course.DepartmentId);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {course.DepartmentId}");

            var existingCourse = await _courseRepository.GetAll<Course>()
                                    .FirstOrDefaultAsync(c => c.Name == course.Name && !c.IsDeleted);
            
            if (existingCourse is not null)
                return new ErrorResult("This course is already registered.");

            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();

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
            
            var teacher = await _courseRepository.GetByIdAsync<Teacher>(course.TeacherId);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResult($"There is no teacher with ID : {course.TeacherId}");

            var department = await _courseRepository.GetByIdAsync<Department>(course.DepartmentId);

            if (department is null || department.IsDeleted)
                return new ErrorResult($"There is no department with ID : {course.DepartmentId}");

            var existingCourse = await _courseRepository.GetAll<Course>()
                                    .FirstOrDefaultAsync(c => c.Name == course.Name && !c.IsDeleted);

            if (existingCourse is not null)
                return new ErrorResult($"This course is already registered.");
            
            await _courseRepository.Add(course);
            await _courseRepository.SaveChangesAsync();

            return new SuccessResult("Course added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }

}