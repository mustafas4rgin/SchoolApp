using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listin;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : GenericController<Course,CourseDTO,CreateCourseDTO,UpdateCourseDTO>
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public CourseController(
            ICourseService service,
            IValidator<CreateCourseDTO> createValidator,
            IValidator<UpdateCourseDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _courseService = service;
            _mapper = mapper;
        }
        [HttpGet("AvailableCourses")]
        public async Task<IActionResult> AvailableCourses([FromQuery]QueryParameters param)
        {
            var result = await _courseService.AvailableCoursesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var courses = result.Data;

            var dto = _mapper.Map<List<CourseDTO>>(courses);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _courseService.GetCoursesWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var courses = result.Data;

            var dto = _mapper.Map<List<CourseDTO>>(courses);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id, [FromQuery]QueryParameters param)
        {
            var result = await _courseService.GetCourseByIdWithIncludesAsync(param, id);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var course = result.Data;

            var dto = _mapper.Map<CourseDTO>(course);

            return Ok(dto);
        }
        [HttpPost("OpenCourse/{courseId}")]
        public async Task<IActionResult> OpenCourse([FromRoute]int courseId)
        {
            var existingCourseResult = await _courseService.GetByIdAsync(courseId);

            var existingCourseErrorResult = HandleServiceResult(existingCourseResult);

            if (existingCourseErrorResult != null)
                return existingCourseErrorResult;

            var existingCourse = existingCourseResult.Data;

            var openingCourseResult = await _courseService.OpenCourseAsync(existingCourse);

            var openingCourseErrorResult = HandleServiceResult(openingCourseResult);

            if (openingCourseErrorResult != null)
                return openingCourseErrorResult;

            return Ok(openingCourseResult.Message);
        }
        [HttpPost("CloseCourse/{courseId}")]
        public async Task<IActionResult> CloseCourse([FromRoute]int courseId)
        {
            var existingCourseResult = await _courseService.GetByIdAsync(courseId);

            var existingCourseErrorResult = HandleServiceResult(existingCourseResult);

            if (existingCourseErrorResult != null)
                return existingCourseErrorResult;

            var existingCourse = existingCourseResult.Data;

            var closingCourseResult = await _courseService.CloseCourseAsync(existingCourse);

            var closingCourseErrorResult = HandleServiceResult(closingCourseResult);

            if (closingCourseErrorResult != null)
                return closingCourseErrorResult;

            return Ok(closingCourseResult.Message);
        }
    }
}
