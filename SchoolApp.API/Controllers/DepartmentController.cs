using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department,DepartmentDTO,CreateDepartmentDTO,UpdateDepartmentDTO>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(
            IDepartmentService service,
            IValidator<CreateDepartmentDTO> createValidator,
            IValidator<UpdateDepartmentDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper)
        {
            _departmentService = service;
            _mapper = mapper;
        }
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _departmentService.GetDepartmentsWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var departments = result.Data;

            var dto = _mapper.Map<List<DepartmentDTO>>(departments);

            return Ok(dto);
        }
        public override async Task<IActionResult> GetById([FromRoute]int id,[FromQuery]QueryParameters param)
        {
            var result = await _departmentService.GetDepartmentByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var department = result.Data;

            var dto = _mapper.Map<DepartmentDTO>(department);

            return Ok(dto);
        }
    }
}
