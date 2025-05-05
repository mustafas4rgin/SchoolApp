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
    }
}
