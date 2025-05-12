using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class SurveyAnswerProfile : Profile
{
    public SurveyAnswerProfile()
    {
        CreateMap<SurveyAnswer, SurveyAnswerDTO>()
            .ForMember(dest => dest.Question, opt => opt.MapFrom(sa => sa.Question.QuestionText));
        CreateMap<SurveyAnswer, CreateSurveyAnswerDTO>().ReverseMap();
        CreateMap<SurveyAnswer, UpdateSurveyAnswerDTO>().ReverseMap();
    }
}