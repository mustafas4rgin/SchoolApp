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
    public class StudentController : GenericController<Student,StudentDTO,CreateStudentDTO,UpdateStudentDTO>
    {
        private readonly IStudentService studentService;
        private readonly IMapper _mapper;
        public StudentController(
            IStudentService service,
            IValidator<CreateStudentDTO> createValidator,
            IValidator<UpdateStudentDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) 
        {
            studentService = service;
            _mapper = mapper;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await studentService.GetStudentsWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var students = result.Data;

            var dto = _mapper.Map<List<StudentDTO>>(students);
            
            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id, [FromQuery]QueryParameters param)
        {
            var result = await studentService.GetStudentByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var student = result.Data;

            var dto = _mapper.Map<StudentDTO>(student);

            return Ok(dto);
        }
        [HttpGet("MyInfo")]
        public async Task<IActionResult> GetMyInfo([FromQuery]QueryParameters param)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return BadRequest("Invalid token.");

            var result = await studentService.GetMyInfo(studentId.Value,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var student = result.Data;

            var dto = _mapper.Map<StudentDTO>(student);

            return Ok(dto);
        }
    }
}
