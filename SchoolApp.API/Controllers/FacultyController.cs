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

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : GenericController<Faculty,FacultyDTO,CreateFacultyDTO,UpdateFacultyDTO>
    {
        private readonly IFacultyService _facultyService;
        private readonly IMapper _mapper;
        public FacultyController(
            IFacultyService service,
            IValidator<CreateFacultyDTO> createValidator,
            IValidator<UpdateFacultyDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper)
        {
            _mapper = mapper;
            _facultyService = service;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _facultyService.GetFacultiesWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var faculties = result.Data;

            var dto = _mapper.Map<List<FacultyDTO>>(faculties);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]QueryParameters param)
        {
            var result = await _facultyService.GetFacultyByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var faculty = result.Data;

            var dto = _mapper.Map<FacultyDTO>(faculty);

            return Ok(dto);
        }
    }
}
