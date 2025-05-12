using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class SurveyQuestionProfile : Profile
{
    public SurveyQuestionProfile()
    {
        CreateMap<SurveyQuestion,SurveyQuestionDTO>().ReverseMap();
        CreateMap<SurveyQuestion,CreateSurveyQuestionDTO>().ReverseMap();
        CreateMap<SurveyQuestion,UpdateSurveyQuestionDTO>().ReverseMap();
    }
}