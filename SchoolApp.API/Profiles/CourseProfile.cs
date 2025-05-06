using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listin;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<CreateCourseDTO, Course>().ReverseMap();

        CreateMap<Course, CourseDTOForTeacher>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Title));

        CreateMap<Course, CourseDTO>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Title))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.FirstName))
            .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src =>
                        src.StudentCourses
                            .Where(sc => !sc.IsDeleted)
                            .Select(sc => sc.Student.FirstName)
                            .ToList()))
            .ForMember(dest => dest.Grades, opt => opt.MapFrom(src =>
                        src.Grades
                            .Where(g => !g.IsDeleted)
                            .Select(g => new GradeDTOForCourse
                            {
                                Id = g.Id,
                                Note = g.Note,
                                StudentName = g.Student.FirstName
                            })));
    }
}