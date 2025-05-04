using AutoMapper;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
        .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
    }
}