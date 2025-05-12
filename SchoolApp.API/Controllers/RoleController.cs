using System.Security.Cryptography.X509Certificates;
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
    public class RoleController : GenericController<Role,RoleDTO,CreateRoleDTO,UpdateRoleDTO>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(
            IRoleService roleService,
            IValidator<CreateRoleDTO> createValidator,
            IValidator<UpdateRoleDTO> updateValidator,
            IMapper mapper
        ) : base(roleService,createValidator,updateValidator,mapper) 
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public override async Task<IActionResult> GetAll([FromQuery]QueryParameters param)
        {
            var result = await _roleService.GetRolesWithIncludesAsync(param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;
            
            var roles = result.Data;

            var dto = _mapper.Map<List<RoleDTO>>(roles);

            return Ok(dto);
        }
        [HttpPost("GetById/{id}")]
        public override async Task<IActionResult> GetById(int id, QueryParameters param)
        {
            var result = await _roleService.GetRoleByIdWithIncludesAsync(id,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var role = result.Data;

            var dto = _mapper.Map<RoleDTO>(role);

            return Ok(dto);
        }
    }
}
