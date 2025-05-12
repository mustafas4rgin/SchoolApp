namespace SchoolApp.Application.DTOs.Listing;

public class SurveyDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<SurveyQuestionDTO> Questions { get; set; } = new();
}