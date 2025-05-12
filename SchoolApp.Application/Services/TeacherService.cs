using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;
using SchoolApp.Domain.Results.WithData;

namespace SchoolApp.Application.Services;

public class TeacherService : GenericService<Teacher>, ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;
    public TeacherService(
        ITeacherRepository teacherRepository,
        IValidator<Teacher> validator
    ) : base (teacherRepository,validator)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<IServiceResultWithData<IEnumerable<Teacher>>> GetTeachersWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _teacherRepository.GetAll<Teacher>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForTeacher(query, param.Include);

            var teachers = await query.Where(t => !t.IsDeleted)
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.FirstName.ToLower().Contains(param.Search.ToLower()))
                            .ToListAsync();

            if (!teachers.Any())
                return new ErrorResultWithData<IEnumerable<Teacher>>("There is no teacher.");

            return new SuccessResultWithData<IEnumerable<Teacher>>("Teachers found.",teachers);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<Teacher>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<Teacher>> GetTeacherByIdWithIncludesAsync(int id, QueryParameters param)
    {
        try
        {
            var query = _teacherRepository.GetAll<Teacher>();

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForTeacher(query,param.Include);

            var teacher = await query
                            .Where(p => string.IsNullOrEmpty(param.Search) || p.FirstName.ToLower().Contains(param.Search.ToLower()))
                            .FirstOrDefaultAsync(q => q.Id == id);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResultWithData<Teacher>($"There is no teacher with ID : {id}");

            return new SuccessResultWithData<Teacher>("Teacher found.",teacher);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<Teacher>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<Course>>> GetTeachersCourses(int teacherId, QueryParameters param)
    {
        try
        {
            var teacher = await _teacherRepository.GetByIdAsync<Teacher>(teacherId);

            if (teacher is null || teacher.IsDeleted)
                return new ErrorResultWithData<IEnumerable<Course>>($"There is no teacher with ID : {teacherId}");

            var query = _teacherRepository.GetTeachersCourses(teacherId);

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForCourse(query,param.Include);

            var courses = await query.Where(c => !c.IsDeleted)
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
}