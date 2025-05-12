using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class ScholarshipApplicationProfile : Profile
{
    public ScholarshipApplicationProfile()
    {
        CreateMap<ScholarshipApplication, UpdateScholarshipDTO>().ReverseMap();
        CreateMap<ScholarshipApplication, ScholarshipApplicationDTO>().ReverseMap();

        CreateMap<CreateScholarshipDTO, ScholarshipApplication>();

    }
}