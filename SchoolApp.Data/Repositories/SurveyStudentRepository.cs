using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class SurveyStudentRepository : GenericRepository, ISurveyStudentRepository
{
    private readonly AppDbContext _context;
    public SurveyStudentRepository(
        AppDbContext context
    ) : base(context)
    {
        _context = context;
    }
    public async Task<bool> IsThereSurveyStudent(int surveyId, int studentId)
    {
        return await _context.Set<SurveyStudent>()
                            .AnyAsync(ss => ss.StudentId == studentId && ss.SurveyId == surveyId);
    }
}