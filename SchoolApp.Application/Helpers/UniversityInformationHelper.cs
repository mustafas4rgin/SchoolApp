using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace SchoolApp.Application.Helpers;

public static class UniverstiyInformationHelper
{
    private static IConfiguration _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static int GetUniversityPrice()
    {
        if (_configuration == null)
        {
            throw new InvalidOperationException("ConfigurationHelper is not initialized.");
        }

        return _configuration.GetValue<int>("University:Price");
    }

    public static int GetCurrentTerm()
    {
        if (_configuration == null)
        {
            throw new InvalidOperationException("ConfigurationHelper is not initialized.");
        }

        var currentDate = DateTime.UtcNow;

        var termStartDates = _configuration.GetSection("University:TermStartDates").GetChildren()
            .Select(x => new
            {
                Term = int.Parse(x.Key),
                StartDate = DateTime.ParseExact(x.Value!, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            })
            .OrderBy(x => x.StartDate)
            .ToList();

        for (int i = termStartDates.Count - 1; i >= 0; i--)
        {
            if (currentDate >= termStartDates[i].StartDate)
            {
                return termStartDates[i].Term;
            }
        }

        return termStartDates.First().Term;
    }
}
