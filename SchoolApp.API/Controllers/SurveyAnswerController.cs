using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyAnswerController : GenericController
    <SurveyAnswer,
    SurveyAnswerDTO,
    CreateSurveyAnswerDTO,
    UpdateSurveyAnswerDTO>
    {
        private readonly ISurveyAnswerService _surveyAnswerService;
        public SurveyAnswerController(
            ISurveyAnswerService service,
            IValidator<CreateSurveyAnswerDTO> createValidator,
            IValidator<UpdateSurveyAnswerDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _surveyAnswerService = service;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _surveyAnswerService.GetSurveyAnswersWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var answers = result.Data;

            var dto = _mapper.Map<List<SurveyAnswerDTO>>(answers);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]QueryParameters param)
        {
            var result = await _surveyAnswerService.GetSurveyAnswerByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var answer = result.Data;

            var dto = _mapper.Map<SurveyAnswerDTO>(answer);

            return Ok(dto);
        }
    }
}
