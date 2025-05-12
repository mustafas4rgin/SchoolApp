using AutoMapper;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<CreateStudentDTO, Student>()
            .ForMember(dest => dest.Hash, opt => opt.Ignore())
            .ForMember(dest => dest.Salt, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                HashingHelper.CreatePasswordHash(src.Password, out var hash, out var salt);
                var studentNumber = NumberGenerator.GenerateStudentNumber();
                dest.Year = 1;
                dest.Hash = hash;
                dest.Salt = salt;
                dest.RoleId = 3;
                dest.Number = studentNumber;
            });

        CreateMap<Student, StudentDTO>()
        .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
        .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src. Department.Title))
        .ForMember(dest => dest.StudentCourses, opt => opt.MapFrom(src =>
                        src.StudentCourses
                            .Where(g => !g.IsDeleted)
                            .Select(g => new CoursesForUserDTO
                            {
                                Id = g.Id,
                                CourseName = g.Course.Name,
                                Attendance = g.Attendance,
                                JoinDate = g.JoinDate,
                                TeacherName = g.Course.Teacher.FirstName + " " + g.Course.Teacher.LastName
                            })));
         
    }
}