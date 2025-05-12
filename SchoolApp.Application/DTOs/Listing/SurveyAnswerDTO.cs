namespace SchoolApp.Application.DTOs.Listing;

public class SurveyAnswerDTO
{
    public int Id { get; set; }
    public string Question { get; set; } = null!;
    public SurveyOptionDTO Option { get; set; } = null!;
}