using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;

namespace SchoolApp.Data.Repositories;

public class SurveyRepository : GenericRepository, ISurveyRepository
{
    public SurveyRepository(AppDbContext context) : base(context) {}
}