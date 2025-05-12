using AutoMapper;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Profiles;

public class TuitionPaymentProfile : Profile
{
    public TuitionPaymentProfile()
    {
        CreateMap<TuitionPayment, TuitionPaymentDto>()
            .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src => src.TotalAmount - src.PaidAmount))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.PaidAmount >= src.TotalAmount ? "Tamamlandı" : "Eksik"));
        
        CreateMap<TuitionPayment, CreateTuitionPaymentDTO>().ReverseMap();
        CreateMap<TuitionPayment, UpdateTuitionDTO>().ReverseMap();
    }
}