using AutoMapper;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class StudentCourseProfile : Profile
{
    public StudentCourseProfile()
    {
        CreateMap<CreateStudentCourseDTO, StudentCourse>().ReverseMap();
        CreateMap<UpdateStudentCourseDTO, StudentCourse>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseDTO>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));
    }
}