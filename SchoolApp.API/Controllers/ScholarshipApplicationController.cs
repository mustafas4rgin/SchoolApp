using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScholarshipApplicationController 
    : GenericController<
    ScholarshipApplication,
    ScholarshipApplicationDTO,
    CreateScholarshipDTO,
    UpdateScholarshipDTO>
    {
        public ScholarshipApplicationController(
            IGenericService<ScholarshipApplication> service,
            IValidator<CreateScholarshipDTO> createValidator,
            IValidator<UpdateScholarshipDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper)
        {}
    }
}
