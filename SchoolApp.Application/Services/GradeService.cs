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

public class GradeService : GenericService<Grade>, IGradeService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IValidator<Grade> _validator;
    public GradeService(
        IGenericRepository genericRepository,
        IValidator<Grade> validator
    ) : base(genericRepository, validator)
    {
        _genericRepository = genericRepository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<IEnumerable<Grade>>> GetGradesByCourseIdAsync(int courseId, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Grade>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForGrade(query,param.Include);

            var grades = await query.Where(g => g.CourseId == courseId && !g.IsDeleted)
                            .ToListAsync();

            if (!grades.Any())
                return new ErrorResultWithData<IEnumerable<Grade>>("There is no grade.");

            return new SuccessResultWithData<IEnumerable<Grade>>("Grades found.", grades);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Grade>>(ex.Message);
        }
    }
    public override async Task<IServiceResult> UpdateAsync(Grade grade)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(grade);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var student = await _genericRepository.GetAll<Student>()
                                    .Include(s => s.StudentCourses.Where(sc => !sc.IsDeleted))
                                    .FirstOrDefaultAsync(s => s.Id == grade.StudentId);

            if (student is null)
                return new ErrorResult($"There is no student with Id : {grade.StudentId}");

            if (student.StudentCourses == null || !student.StudentCourses.Any())
                return new ErrorResult("This student is not taking any courses.");

            var course = student.StudentCourses.FirstOrDefault(sc => sc.CourseId == grade.CourseId && !sc.IsDeleted);

            if (course is null)
                return new ErrorResult($"Student is not taking this course.");

            await _genericRepository.UpdateAsync(grade);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Grade updated.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<Grade>>> GetStudentsGradesAsync(int studentId, QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Grade>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForGrade(query,param.Include);

            var grades = await query.Where(g => !g.IsDeleted && g.StudentId == studentId)
                                .ToListAsync();

            if (!grades.Any())
                return new ErrorResultWithData<IEnumerable<Grade>>("This student has no grades");

            return new SuccessResultWithData<IEnumerable<Grade>>("Grades found",grades);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Grade>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<Grade>>> GetGradesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _genericRepository.GetAll<Grade>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForGrade(query, param.Include);

            var grades = await query.Where(g => !g.IsDeleted)
                                .ToListAsync();

            if (!grades.Any())
                return new ErrorResultWithData<IEnumerable<Grade>>("There is no grade.");

            return new SuccessResultWithData<IEnumerable<Grade>>("Grades found.", grades);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Grade>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Grade>> GetGradeByIdWithIncludesAsync(QueryParameters param, int id)
    {
        try
        {
            var query = _genericRepository.GetAll<Grade>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForGrade(query, param.Include);

            var grade = await query.FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);

            if (grade is null)
                return new ErrorResultWithData<Grade>($"There is no grade with ID : {id}");

            return new SuccessResultWithData<Grade>("Grade found.", grade);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Grade>(ex.Message);
        }
    }
    public override async Task<IServiceResult> AddAsync(Grade grade)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(grade);

            if (!validationResult.IsValid)
                return new ErrorResult(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var student = await _genericRepository.GetAll<Student>()
                                    .Include(s => s.StudentCourses.Where(sc => !sc.IsDeleted))
                                    .FirstOrDefaultAsync(s => s.Id == grade.StudentId);

            if (student is null)
                return new ErrorResult($"There is no student with Id : {grade.StudentId}");

            if (student.StudentCourses == null || !student.StudentCourses.Any())
                return new ErrorResult("This student is not taking any courses.");

            var course = student.StudentCourses.FirstOrDefault(sc => sc.CourseId == grade.CourseId && !sc.IsDeleted);

            if (course is null)
                return new ErrorResult($"Student is not taking this course.");

            await _genericRepository.Add(grade);
            await _genericRepository.SaveChangesAsync();

            return new SuccessResult("Grade added.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }

    }
    public override Task<IServiceResultWithData<IEnumerable<Grade>>> GetAllAsync()
    {
        throw new NotSupportedException("Use GetGradesWithIncludesAsync instead."); 
    }
}