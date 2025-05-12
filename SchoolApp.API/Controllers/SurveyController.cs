using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Controllers;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : GenericController
    <Survey,
    SurveyDTO,
    CreateSurveyDTO,
    UpdateSurveyDTO>
    {
        public SurveyController(
            ISurveyService service,
            IValidator<CreateSurveyDTO> createValidator,
            IValidator<UpdateSurveyDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper)
        {}
    }
}
