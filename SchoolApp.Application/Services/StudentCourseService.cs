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

public class StudentCourseService : GenericService<StudentCourse>, IStudentCourseService
{
    private readonly IValidator<StudentCourse> _validator;
    private readonly IGenericRepository _genericRepository;
    public StudentCourseService(
        IGenericRepository genericRepository,
        IValidator<StudentCourse> validator
    ) : base(genericRepository, validator)
    {
        _genericRepository = genericRepository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<StudentCourse>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudentCourses(query, param.Include);

            var studentCourses = await query.Where(sc => !sc.IsDeleted)
                                    .ToListAsync();

            if (!studentCourses.Any())
                return new ErrorResultWithData<IEnumerable<StudentCourse>>("There is no studentcourse.");

            return new SuccessResultWithData<IEnumerable<StudentCourse>>("Studentcourses found.", studentCourses);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<StudentCourse>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<StudentCourse>> GetStudentCourseByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<StudentCourse>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudentCourses(query, param.Include);

            var studentCourse = await query.FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted);

            if (studentCourse is null)
                return new ErrorResultWithData<StudentCourse>($"There is no studentcourse with ID : {id}");

            return new SuccessResultWithData<StudentCourse>("Student course found.", studentCourse);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<StudentCourse>(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(StudentCourse studentCourse)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(studentCourse);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existingStudentCourse = await _genericRepository.GetAll<StudentCourse>()
                                        .FirstOrDefaultAsync(esc => esc.StudentId == studentCourse.StudentId
                                        && esc.CourseId == studentCourse.CourseId);

            if (existingStudentCourse is not null && !existingStudentCourse.IsDeleted)
                return new ErrorResult("Student already registered for this course.");

            if (existingStudentCourse is not null && existingStudentCourse.IsDeleted)
            {
                existingStudentCourse.IsDeleted = false;
                await _genericRepository.SaveChangesAsync();
                return new SuccessResult("Student course reactivated.");
            }

            var student = await _genericRepository.GetByIdAsync<Student>(studentCourse.StudentId);

            if (student is null || student.IsDeleted)
                return new ErrorResult($"There is no student with ID : {studentCourse.StudentId}");

            var course = await _genericRepository.GetByIdAsync<Course>(studentCourse.CourseId);

            if (course is null || course.IsDeleted)
                return new ErrorResult($"There is no course with ID : {studentCourse.CourseId}");

            await _genericRepository.Add(studentCourse);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Student course added successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public override async Task<IServiceResult> UpdateAsync(StudentCourse studentCourse)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(studentCourse);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existingStudentCourse = await _genericRepository.GetAll<StudentCourse>()
                                        .FirstOrDefaultAsync(esc => esc.StudentId == studentCourse.StudentId
                                        && esc.CourseId == studentCourse.CourseId);

            if (existingStudentCourse is not null && !existingStudentCourse.IsDeleted)
                return new ErrorResult("Student already registered for this course.");

            if (existingStudentCourse is not null && existingStudentCourse.IsDeleted)
            {
                existingStudentCourse.IsDeleted = false;
                await _genericRepository.SaveChangesAsync();
                return new SuccessResult("Student course reactivated.");
            }

            var student = await _genericRepository.GetByIdAsync<Student>(studentCourse.StudentId);

            if (student is null || student.IsDeleted)
                return new ErrorResult($"There is no student with ID : {studentCourse.StudentId}");

            var course = await _genericRepository.GetByIdAsync<Course>(studentCourse.CourseId);

            if (course is null || course.IsDeleted)
                return new ErrorResult($"There is no course with ID : {studentCourse.CourseId}");

            await _genericRepository.UpdateAsync(studentCourse);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Student course updated successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
}