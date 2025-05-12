using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Controllers;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyOptionController : GenericController
    <SurveyOption,
    SurveyOptionDTO,
    CreateSurveyOptionDTO,
    UpdateSurveyOptionDTO>
    {
        public SurveyOptionController(
            IGenericService<SurveyOption> service,
            IValidator<CreateSurveyOptionDTO> createValidator,
            IValidator<UpdateSurveyOptionDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) {}
    }
}
