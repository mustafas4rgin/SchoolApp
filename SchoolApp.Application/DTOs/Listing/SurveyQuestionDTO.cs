namespace SchoolApp.Application.DTOs.Listing;

public class SurveyQuestionDTO
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<SurveyOptionDTO> Options { get; set; } = null!;
}