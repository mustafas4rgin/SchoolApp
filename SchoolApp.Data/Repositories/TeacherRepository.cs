using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class TeacherRepository : GenericRepository, ITeacherRepository
{
    private readonly AppDbContext _context;
    public TeacherRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public IQueryable<Course> GetTeachersCourses(int teacherId)
    {
        return _context.Set<Course>().Where(c => c.TeacherId == teacherId);
    }
}