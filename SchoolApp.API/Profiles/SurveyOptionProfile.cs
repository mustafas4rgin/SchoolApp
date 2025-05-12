using AutoMapper;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class SurveyOptionProfile : Profile
{
    public SurveyOptionProfile()
    {
        CreateMap<SurveyOption, CreateSurveyOptionDTO>().ReverseMap();
        CreateMap<SurveyOption, UpdateSurveyOptionDTO>().ReverseMap();
        CreateMap<SurveyOption, SurveyOptionDTO>().ReverseMap();
    }
}