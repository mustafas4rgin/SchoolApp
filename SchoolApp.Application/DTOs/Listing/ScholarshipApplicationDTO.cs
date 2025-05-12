namespace SchoolApp.Application.DTOs.Listing;

public class ScholarshipApplicationDTO
{
    public string FullName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string IncomeStatus { get; set; } = string.Empty;
    public int SiblingCount { get; set; }
    public string? Note { get; set; }
    
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;

    // Navigation properties
    public StudentDTO Student { get; set; } = null!;

}