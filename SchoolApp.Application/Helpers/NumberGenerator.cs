namespace SchoolApp.Application.Helpers;

public static class NumberGenerator
{
    private static readonly Random _random = new();

    public static string GenerateStudentNumber()
    {
        int currentYear = DateTime.UtcNow.Year;
        int randomNumber = _random.Next(100000, 1000000);
        return $"{currentYear}{randomNumber}";
    }
    public static string GenerateTeacherNumber()
    {
        int currentYear = DateTime.UtcNow.Year;
        int randomNumber = _random.Next(100000, 1000000);
        return $"{currentYear}{randomNumber}";
    }
}