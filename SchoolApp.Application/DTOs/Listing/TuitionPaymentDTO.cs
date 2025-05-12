using SchoolApp.Domain.Enums;

namespace SchoolApp.Application.DTOs.Listing;
public class TuitionPaymentDto
{
    public string Term => $"{TermType} {Year}";
    public TermType TermType { get; set; }
    public int Year { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal RemainingAmount => TotalAmount - PaidAmount;
    public string Status => PaidAmount >= TotalAmount ? "Tamamlandı" : "Eksik";
    public DateTime LastPaymentDate { get; set; }
}
