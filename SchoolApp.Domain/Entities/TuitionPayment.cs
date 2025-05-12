using SchoolApp.Domain.Enums;

namespace SchoolApp.Domain.Entities;

public class TuitionPayment : EntityBase
{
    public int StudentId { get; set; }
    public TermType TermType { get; set; } 
    public int Year { get; set; } = DateTime.UtcNow.Year;
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime LastPaymentDate { get; set; } = DateTime.UtcNow.AddMonths(1);

    // Hesaplanabilir property
    public decimal RemainingAmount => TotalAmount - PaidAmount;
    public string Status => PaidAmount >= TotalAmount ? "Tamamlandı" : "Eksik";
    
    public Student Student { get; set; } = null!;
}
