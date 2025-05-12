using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class StudentRepository : GenericRepository, IStudentRepository
{
    private readonly AppDbContext _context;
    public StudentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}