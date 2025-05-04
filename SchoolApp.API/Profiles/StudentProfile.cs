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
        .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
         
    }
}