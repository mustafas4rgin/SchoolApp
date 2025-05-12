using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Helpers;

public static class QueryHelper
{
    public static IQueryable<TuitionPayment> ApplyIncludesForTuitionPayment(IQueryable<TuitionPayment> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "student" : query = query.Include(tp => tp.Student);break;
            }
        }
        return query;
    }
    public static IQueryable<SurveyAnswer> ApplyIncludesForSurveyAnswer(IQueryable<SurveyAnswer> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "question" : query = query.Include(sa => sa.Question);break;
                case "option" : query = query.Include(sa => sa.Option);break;
                case "all" : query = query.Include(sa => sa.Question)
                                        .Include(sa => sa.Option);
                                        break;
            }
        }
        return query;
    }
    public static IQueryable<SurveyQuestion> ApplyIncludesForSurveyQuestion(IQueryable<SurveyQuestion> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "options" : query = query.Include(sq => sq.Options);break;
            }
        }
        return query;
    }
    public static IQueryable<Department> ApplyIncludesForDepartment(IQueryable<Department> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "all" : query = query.Include(f => f.Faculty)
                                        .Include(f => f.Teachers)
                                        .Include(f => f.Students)
                                        .Include(f => f.Courses);
                                        break;
                case "faculty" : query = query.Include(f => f.Faculty);break;
                case "students" : query = query.Include(f => f.Students);break;
                case "teachers" : query = query.Include(f => f.Teachers);break;
                case "courses" : query = query.Include(f => f.Courses);break;
            }
        }
        return query;
    }
    public static IQueryable<Faculty> ApplyIncludesForFaculty(IQueryable<Faculty> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "departments" : query = query.Include(f => f.Departments);break;
            }
        }
        return query;
    }
    public static IQueryable<Student> ApplyIncludesForStudent(IQueryable<Student> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            if (inc == "all")
                query = query.Include(s => s.Role)
                            .Include(s => s.StudentCourses)
                                .ThenInclude(sc => sc.Course)
                                .ThenInclude(sc => sc.Teacher)
                            .Include(s => s.Grades)
                                .ThenInclude(g => g.Course)
                            .Include(s => s.Department);
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
                else if (inc == "department")
                    query = query.Include(s => s.Department);
            }
        }
        return query;
    }
    public static IQueryable<Teacher> ApplyIncludesForTeacher(IQueryable<Teacher> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "courses": query = query.Include(t => t.Courses); break;
                case "department": query = query.Include(t => t.Department); break;
                case "all": query =query.Include(t => t.Department).Include(t => t.Courses);break;
            }
        }

        return query;
    }
    public static IQueryable<StudentCourse> ApplyIncludesForStudentCourses(IQueryable<StudentCourse> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "student": query = query.Include(sc => sc.Student); break;
                case "course": query = query.Include(sc => sc.Course); break;
                case "all": query = query.Include(sc => sc.Course).Include(sc => sc.Student); break;
            }
        }
        return query;
    }
    public static IQueryable<Course> ApplyIncludesForCourse(IQueryable<Course> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "department":
                    query = query.Include(c => c.Department);
                    break;
                case "students":
                    query = query.Include(c => c.StudentCourses)
                                    .ThenInclude(sc => sc.Student);
                    break;
                case "grades":
                    query = query.Include(c => c.Grades)
                                .ThenInclude(g => g.Student);
                    break;
                case "teacher":
                    query = query.Include(c => c.Teacher);
                    break;
                case "all":
                    query = query.Include(c => c.Teacher)
                            .Include(c => c.Grades)
                                .ThenInclude(g => g.Student)
                            .Include(c => c.StudentCourses)
                                .ThenInclude(sc => sc.Student)
                            .Include(c => c.Department);
                    break;
            }
        }

        return query;
    }
    public static IQueryable<Grade> ApplyIncludesForGrade(IQueryable<Grade> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "course": query = query.Include(g => g.Course); break;
                case "student": query = query.Include(g => g.Student); break;
                case "all": query = query.Include(g => g.Student).Include(g => g.Course); break;
            }
        }

        return query;
    }
}