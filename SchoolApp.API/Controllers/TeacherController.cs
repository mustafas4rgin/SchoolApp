using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Create;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : GenericController<Teacher,TeacherDTO,CreateTeacherDTO,UpdateTeacherDTO>
    {
        public TeacherController(
            IGenericService<Teacher> service,
            IValidator<CreateTeacherDTO> createValidator,
            IValidator<UpdateTeacherDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) {}
    }
}
