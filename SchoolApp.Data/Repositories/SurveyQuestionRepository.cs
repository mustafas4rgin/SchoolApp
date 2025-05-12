using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class SurveyQuestionRepository : GenericRepository, ISurveyQuestionRepository
{
    private readonly AppDbContext _context;
    public SurveyQuestionRepository(
        AppDbContext context
    ) : base(context)
    {
        _context = context;
    }
    public IQueryable<SurveyQuestion> GetAllBySurveyId(int surveyId)
    {
        return _context.Set<SurveyQuestion>().Where(sq => sq.SurveyId == surveyId);
    }
}