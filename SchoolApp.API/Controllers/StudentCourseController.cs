using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Application.DTOs.Listing;
using SchoolApp.Application.DTOs.Update;
using SchoolApp.Application.Services;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : GenericController<StudentCourse,StudentCourseDTO,CreateStudentCourseDTO,UpdateStudentCourseDTO>
    {
        public StudentCourseController(
            IGenericService<StudentCourse> service,
            IValidator<CreateStudentCourseDTO> createValidator,
            IValidator<UpdateStudentCourseDTO> updateValidator,
            IMapper mapper
        ) : base(service,createValidator,updateValidator,mapper) {}
    }
}
