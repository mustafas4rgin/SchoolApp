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
    public class GradeController : GenericController<Grade,GradeDTO,CreateGradeDTO,UpdateGradeDTO>
    {
        private readonly IValidator<CreateGradeDTO> _createValidator;
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;
        public GradeController(
            IGradeService service,
            IValidator<CreateGradeDTO> createValidator,
            IValidator<UpdateGradeDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _gradeService = service;
            _createValidator = createValidator;
            _mapper = mapper;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _gradeService.GetGradesWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var grades = result.Data;

            var dto = _mapper.Map<List<GradeDTO>>(grades);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id, [FromQuery]QueryParameters param)
        {
            var result = await _gradeService.GetGradeByIdWithIncludesAsync(param,id);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var grade = result.Data;

            var dto = _mapper.Map<GradeDTO>(grade);

            return Ok(dto);
        }
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentGrades([FromQuery] QueryParameters param, [FromRoute]int studentId)
        {
            var result = await _gradeService.GetStudentsGradesAsync(studentId,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var gradesByStudentId = result.Data;

            var dto = _mapper.Map<List<GradesForStudentDTO>>(gradesByStudentId);

            return Ok(dto);
        }
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetGradesByCourseId([FromQuery]QueryParameters param,[FromRoute]int courseId)
        {
            var result = await _gradeService.GetGradesByCourseIdAsync(courseId,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var grades = result.Data;

            var dto = _mapper.Map<List<GradeDTO>>(grades);

            return Ok(dto);
        }
    }
}
