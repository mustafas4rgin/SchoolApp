using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listin;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDTO, Course>().ReverseMap();
        CreateMap<CourseDTO, Course>().ReverseMap();
    }
}