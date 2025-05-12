using FluentValidation;
using SchoolApp.Application.Concrete;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Application.Services;

public class SurveyService : GenericService<Survey>, ISurveyService
{
    public SurveyService(
        ISurveyRepository surveyRepository,
        IValidator<Survey> validator
    ) : base(surveyRepository,validator) {}
}