using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Requests;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Application.Services;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : GenericController<StudentCourse,StudentCourseDTO,CreateStudentCourseDTO,UpdateStudentCourseDTO>
    {
        private readonly IStudentCourseService _studentCourseService;
        private readonly IMapper _mapper;
        public StudentCourseController(
            IStudentCourseService service,
            IValidator<CreateStudentCourseDTO> createValidator,
            IValidator<UpdateStudentCourseDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _mapper = mapper;
            _studentCourseService = service;
        }
        [HttpGet("CurrentYearsCourses")]
        public async Task<IActionResult> GetCoursesByStudentsCurrentYear([FromQuery]QueryParameters param)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _studentCourseService.GetStudentCoursesByStudentsCurrentYear(studentId.Value,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var currentYearCourses = result.Data;

            var dto = _mapper.Map<List<StudentCourseDTO>>(currentYearCourses);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _studentCourseService.GetStudentCoursesWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var studentCourses = result.Data;

            var dto = _mapper.Map<List<StudentCourseDTO>>(studentCourses);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]QueryParameters param)
        {
            var result = await _studentCourseService.GetStudentCourseByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var studentCourse = result.Data;

            var dto = _mapper.Map<StudentCourseDTO>(studentCourse);

            return Ok(dto);
        }
        [HttpPost("SelectCourses")]
        public async Task<IActionResult> SelectCourses([FromBody]SelectCoursesRequest request)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _studentCourseService.SelectCoursesAsync(studentId.Value,request.CourseIds);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok(result.Message);
        }
        [HttpPut("ConfirmSelection")]
        public async Task<IActionResult> ConfirmSelections()
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _studentCourseService.ConfirmSelectionAsync(studentId.Value);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok(result.Message);
        }
        [HttpGet("Unconfirmed")]
        public async Task<IActionResult> UnconfirmedSelections([FromQuery]QueryParameters param)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _studentCourseService.GetUnconfirmedSelectionsAsync(studentId.Value,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var studentCourses = result.Data;

            var dto = _mapper.Map<List<StudentCourseDTO>>(studentCourses);

            return Ok(dto);
        }
    }
}
