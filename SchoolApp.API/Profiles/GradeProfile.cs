using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class GradeProfile : Profile
{
    public GradeProfile()
    {
        CreateMap<CreateGradeDTO, Grade>().ReverseMap();
        CreateMap<UpdateGradeDTO, Grade>().ReverseMap();
        CreateMap<Grade, GradeDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName));

        CreateMap<Grade, GradesForStudentDTO>()
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));
    }
}