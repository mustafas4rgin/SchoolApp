using SchoolApp.Domain.Enums;

namespace SchoolApp.Domain.Entities;

public class ScholarshipApplication : EntityBase
{
    public string FullName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string IncomeStatus { get; set; } = string.Empty;
    public int SiblingCount { get; set; }
    public string? Note { get; set; }
    
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public ScholarshipStatus Status { get; set; } = ScholarshipStatus.Beklemede;

    // Foreign key
    public int StudentId { get; set; }

    // Navigation properties
    public Student Student { get; set; } = null!;
}
