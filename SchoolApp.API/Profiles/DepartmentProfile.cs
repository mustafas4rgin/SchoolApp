using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentDTO,Department>().ReverseMap();
        CreateMap<CreateDepartmentDTO,Department>().ReverseMap();
        CreateMap<UpdateDepartmentDTO,Department>().ReverseMap();
    }
}