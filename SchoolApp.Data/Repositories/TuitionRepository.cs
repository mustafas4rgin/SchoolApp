using SchoolApp.Data.Contexts;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Repositories;

public class TuitionRepository : GenericRepository, ITuitionRepository
{
    private readonly AppDbContext _context;
    public TuitionRepository
    (AppDbContext context) : base(context) 
    {
        _context = context;
    }
    public void Payment(TuitionPayment payment, int amount)
    {
        if (payment is null)
            return;

        payment.PaidAmount += amount;

        _context.Set<TuitionPayment>().Update(payment);
    }
}