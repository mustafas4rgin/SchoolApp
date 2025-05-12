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
    private readonly IStudentCourseRepository _studentCourseRepository;
    public StudentCourseService(
        IStudentCourseRepository genericRepository,
        IValidator<StudentCourse> validator
    ) : base(genericRepository, validator)
    {
        _studentCourseRepository = genericRepository;
        _validator = validator;
    }
    public async Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesByStudentsCurrentYear(int studentId, QueryParameters param)
    {
        try
        {
            var student = await _studentCourseRepository.GetByIdAsync<Student>(studentId);

            if (student is null)
                return new ErrorResultWithData<IEnumerable<StudentCourse>>("Auth error.");

            var query = _studentCourseRepository.GetCoursesByStudentsCurrentYear(studentId, student.Year);

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudentCourses(query, param.Include);

            var studentCoursesByYear = await query.ToListAsync();

            if (!studentCoursesByYear.Any())
                return new ErrorResultWithData<IEnumerable<StudentCourse>>("There is no course.");

            return new SuccessResultWithData<IEnumerable<StudentCourse>>("Courses found", studentCoursesByYear);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<StudentCourse>>(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetStudentCoursesWithIncludesAsync(QueryParameters param)
    {
        try
        {
            var query = _studentCourseRepository.GetAll<StudentCourse>();

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
            var query = _studentCourseRepository.GetAll<StudentCourse>();

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

            var existingStudentCourse = await _studentCourseRepository.GetAll<StudentCourse>()
                                        .FirstOrDefaultAsync(esc => esc.StudentId == studentCourse.StudentId
                                        && esc.CourseId == studentCourse.CourseId);

            if (existingStudentCourse is not null && !existingStudentCourse.IsDeleted)
                return new ErrorResult("Student already registered for this course.");

            if (existingStudentCourse is not null && existingStudentCourse.IsDeleted)
            {
                existingStudentCourse.IsDeleted = false;
                await _studentCourseRepository.SaveChangesAsync();
                return new SuccessResult("Student course reactivated.");
            }

            var student = await _studentCourseRepository.GetByIdAsync<Student>(studentCourse.StudentId);

            if (student is null || student.IsDeleted)
                return new ErrorResult($"There is no student with ID : {studentCourse.StudentId}");

            var course = await _studentCourseRepository.GetByIdAsync<Course>(studentCourse.CourseId);

            if (course is null || course.IsDeleted)
                return new ErrorResult($"There is no course with ID : {studentCourse.CourseId}");

            if (course.Quota <= 0)
                return new ErrorResult("There is no quota for this course.");

            await _studentCourseRepository.Add(studentCourse);

            course.Quota -= 1;

            if (course.Quota == 0)
                course.IsAvailable = false;

            await _studentCourseRepository.SaveChangesAsync();

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

            var existingStudentCourse = await _studentCourseRepository.GetAll<StudentCourse>()
                                        .FirstOrDefaultAsync(esc => esc.StudentId == studentCourse.StudentId
                                        && esc.CourseId == studentCourse.CourseId);

            if (existingStudentCourse is not null && !existingStudentCourse.IsDeleted)
                return new ErrorResult("Student already registered for this course.");

            if (existingStudentCourse is not null && existingStudentCourse.IsDeleted)
            {
                existingStudentCourse.IsDeleted = false;
                await _studentCourseRepository.SaveChangesAsync();
                return new SuccessResult("Student course reactivated.");
            }

            var student = await _studentCourseRepository.GetByIdAsync<Student>(studentCourse.StudentId);

            if (student is null || student.IsDeleted)
                return new ErrorResult($"There is no student with ID : {studentCourse.StudentId}");

            var course = await _studentCourseRepository.GetByIdAsync<Course>(studentCourse.CourseId);

            if (course is null || course.IsDeleted)
                return new ErrorResult($"There is no course with ID : {studentCourse.CourseId}");

            await _studentCourseRepository.UpdateAsync(studentCourse);
            await _studentCourseRepository.SaveChangesAsync();

            return new SuccessResult("Student course updated successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResultWithData<IEnumerable<StudentCourse>>> GetUnconfirmedSelectionsAsync(int studentId, QueryParameters param)
    {
        try
        {
            var query = _studentCourseRepository.GetUnconfirmedSelections(studentId);

            if (!string.IsNullOrWhiteSpace(param.Include))
                query = QueryHelper.ApplyIncludesForStudentCourses(query, param.Include);

            var unconfirmedSelections = await query.Where(sc => !sc.IsDeleted)
                                                .ToListAsync();

            if (!unconfirmedSelections.Any())
                return new ErrorResultWithData<IEnumerable<StudentCourse>>("There is no unconfirmed selection.");

            return new SuccessResultWithData<IEnumerable<StudentCourse>>("Unconfirmed selections found.", unconfirmedSelections);
        }
        catch (Exception ex)
        {
            return new ErrorResultWithData<IEnumerable<StudentCourse>>(ex.Message);
        }
    }
    public async Task<IServiceResult> ConfirmSelectionAsync(int studentId)
    {
        try
        {
            var pendingSelections = _studentCourseRepository
                                .GetAll<StudentCourse>()
                                .Where(sc => sc.StudentId == studentId && !sc.IsConfirmed)
                                .ToList();

            foreach (var selection in pendingSelections)
                selection.IsConfirmed = true;

            _studentCourseRepository.UpdateEntities(pendingSelections);
            await _studentCourseRepository.SaveChangesAsync();

            return new SuccessResult("Course selections confirmed successfully.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }
    public async Task<IServiceResult> SelectCoursesAsync(int studentId, List<int> courseIds)
    {
        try
        {
            var alreadySelectedIds = _studentCourseRepository
                .GetAll<StudentCourse>()
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.CourseId)
                .ToHashSet();

            var duplicateIds = courseIds.Where(id => alreadySelectedIds.Contains(id)).ToList();

            if (duplicateIds.Any())
            {
                return new ErrorResult("Zaten kayıtlı olduğunuz ders(ler) var.");
            }

            var newSelections = courseIds.Select(courseId => new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId,
                JoinDate = DateTime.UtcNow,
                IsConfirmed = false
            });

            await _studentCourseRepository.AddEntitiesAsync(newSelections);
            await _studentCourseRepository.SaveChangesAsync();

            return new SuccessResult("Ders seçimi başarıyla kaydedildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult($"Hata: {ex.Message}");
        }
    }
}