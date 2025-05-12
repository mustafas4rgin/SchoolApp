using SchoolApp.Application.DTOs.Listing;

namespace SchoolApp.Application.DTOs.Create;

public class CreateScholarshipDTO 
{
    public string FullName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string IncomeStatus { get; set; } = string.Empty;
    public int SiblingCount { get; set; }
    public string? Note { get; set; }
    public int StudentId { get; set; }

}