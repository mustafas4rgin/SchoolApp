using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
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
        public CourseController(
            IGenericService<Course> service,
            IValidator<CreateCourseDTO> createValidator,
            IValidator<UpdateCourseDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) {}
    }
}
