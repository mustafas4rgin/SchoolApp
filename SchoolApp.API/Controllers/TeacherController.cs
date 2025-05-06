using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : GenericController<Teacher,TeacherDTO,CreateTeacherDTO,UpdateTeacherDTO>
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeacherController(
            ITeacherService service,
            IValidator<CreateTeacherDTO> createValidator,
            IValidator<UpdateTeacherDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            _teacherService = service;
            _mapper = mapper;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _teacherService.GetTeachersWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var teachers = result.Data;

            var dto = _mapper.Map<List<TeacherDTO>>(teachers);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]QueryParameters param)
        {
            var result = await _teacherService.GetTeacherByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var teacher = result.Data;

            var dto = _mapper.Map<TeacherDTO>(teacher);

            return Ok(dto);
        }
    }
}
