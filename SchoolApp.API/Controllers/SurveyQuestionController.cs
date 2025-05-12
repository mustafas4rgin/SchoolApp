using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Controllers;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionController : GenericController
    <SurveyQuestion,
    SurveyQuestionDTO,
    CreateSurveyQuestionDTO,
    UpdateSurveyQuestionDTO>
    {
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly IMapper _mapper;
        public SurveyQuestionController(
            ISurveyQuestionService service,
            IValidator<CreateSurveyQuestionDTO> createValidator,
            IValidator<UpdateSurveyQuestionDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _surveyQuestionService = service;
            _mapper = mapper;
        }
        [HttpGet("GetAll/{surveyId}")]
        public async Task<IActionResult> GetAllBySurveyId([FromRoute]int surveyId,[FromQuery]QueryParameters param)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionsBySurveyIdWithIncludesAsync(surveyId,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var questions = result.Data;

            var dto = _mapper.Map<List<SurveyQuestionDTO>>(questions);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _surveyQuestionService.GetSurveyQuestionsWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var questions = result.Data;

            var dto = _mapper.Map<List<SurveyQuestionDTO>>(questions);

            return Ok(dto);
        }
    }
}
