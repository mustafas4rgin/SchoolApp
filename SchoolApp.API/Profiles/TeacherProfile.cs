using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.Helpers;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<CreateTeacherDTO, Teacher>()
            .ForMember(dest => dest.Hash, opt => opt.Ignore())
            .ForMember(dest => dest.Salt, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                HashingHelper.CreatePasswordHash(src.Password,out var passwordHash, out var passwordSalt);
                var teacherNumber = NumberGenerator.GenerateTeacherNumber();
                dest.Salt = passwordSalt;
                dest.Hash = passwordHash;
                dest.Number = teacherNumber;
                dest.RoleId = 2;
            });

        CreateMap<TeacherDTO, Teacher>().ReverseMap();
    }
}