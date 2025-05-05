using Microsoft.EntityFrameworkCore;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Helpers;

public static class QueryHelper
{
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