using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class CourseRepository : GenericRepository, ICourseRepository
{
    private readonly AppDbContext _context;
    public CourseRepository(
        AppDbContext context
    ) : base(context)
    {
        _context = context;
    }
    public IQueryable<Course> AvailableCourses()
    {
        return _context.Set<Course>().Where(c => c.IsAvailable && !c.IsDeleted);
    }
}