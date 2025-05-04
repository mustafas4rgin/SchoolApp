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
    public class GradeController : GenericController<Grade,GradeDTO,CreateGradeDTO,UpdateGradeDTO>
    {
        public GradeController(
            IGenericService<Grade> service,
            IValidator<CreateGradeDTO> createValidator,
            IValidator<UpdateGradeDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) {}
    }
}
