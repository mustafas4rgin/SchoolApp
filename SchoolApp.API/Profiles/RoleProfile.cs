using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role,CreateRoleDTO>().ReverseMap();
        CreateMap<Role,UpdateRoleDTO>().ReverseMap();
        CreateMap<Role,RoleDTO>().ReverseMap();
    }
}