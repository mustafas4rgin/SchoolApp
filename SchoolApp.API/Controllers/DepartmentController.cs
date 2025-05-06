using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : GenericController<Department,DepartmentDTO,CreateDepartmentDTO,UpdateDepartmentDTO>
    {
        public DepartmentController(
            IGenericService<Department> service,
            IValidator<CreateDepartmentDTO> createValidator,
            IValidator<UpdateDepartmentDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper)
        {
            
        }
    }
}
