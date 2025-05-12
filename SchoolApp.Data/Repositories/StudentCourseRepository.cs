using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class StudentCourseRepository : GenericRepository, IStudentCourseRepository
{
    private readonly AppDbContext _context;
    public StudentCourseRepository(
        AppDbContext context
    ) : base(context)
    {
        _context = context;
    }
    public IQueryable<StudentCourse> GetUnconfirmedSelections(int studentId)
    {
        return _context.Set<StudentCourse>()
                        .Where(sc => sc.StudentId == studentId && !sc.IsConfirmed);
    }
    public IQueryable<StudentCourse> GetCoursesByStudentsCurrentYear(int studentId, int studentYear)
    {
        return _context.Set<StudentCourse>()
                        .Where(sc => sc.StudentId == studentId && sc.Course.Year == studentYear)
                        .Include(sc => sc.Course);
    }
}