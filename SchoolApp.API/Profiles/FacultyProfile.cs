using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class FacultyProfile : Profile
{
    public FacultyProfile()
    {
        CreateMap<FacultyDTO,Faculty>().ReverseMap();
        CreateMap<CreateFacultyDTO,Faculty>().ReverseMap();
        CreateMap<UpdateFacultyDTO,Faculty>().ReverseMap();
    }
}