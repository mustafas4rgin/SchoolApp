using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Helpers;

public static class QueryHelper
{
    public static IQueryable<Course> ApplyIncludesForCourse(IQueryable<Course> query, string include)
    {
        if (string.IsNullOrWhiteSpace(include)) return query;

        var includes = include.Split(',',StringSplitOptions.RemoveEmptyEntries);

        foreach (var inc in includes.Select(i => i.Trim().ToLower()))
        {
            switch (inc)
            {
                case "students" : query = query.Include(c => c.StudentCourses)
                                    .ThenInclude(sc => sc.Student);
                                    break;
                case "grades" : query = query.Include(c => c.Grades)
                                .ThenInclude(g => g.Student);
                                break;
                case "teacher" : query = query.Include(c => c.Teacher);
                break;
                case "all" :query = query.Include(c => c.Teacher)
                            .Include(c => c.Grades)
                                .ThenInclude(g => g.Student)
                            .Include(c => c.StudentCourses)
                                .ThenInclude(sc => sc.Student);
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