using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<Survey,SurveyDTO>().ReverseMap();
        CreateMap<Survey,CreateSurveyDTO>().ReverseMap();
        CreateMap<Survey,UpdateSurveyDTO>().ReverseMap();
    }
}