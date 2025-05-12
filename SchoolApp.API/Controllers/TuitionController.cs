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
using SchoolApp.Application.DTOValidators.Update;
using SchoolApp.Domain.Entities;
using SchoolApp.Domain.Results;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuitionController : GenericController
    <TuitionPayment,
    TuitionPaymentDto,
    CreateTuitionPaymentDTO,
    UpdateTuitionDTO>
    {
        private readonly ITuitionService _tuitionService;
        public TuitionController(
            ITuitionService tuitionService,
            IValidator<CreateTuitionPaymentDTO> createValidator,
            IValidator<UpdateTuitionDTO> updateValidator,
            IMapper mapper
        ) : base(tuitionService,createValidator,updateValidator,mapper)
        {
            _tuitionService = tuitionService;

        }
        [HttpGet("MyPayments")]
        public async Task<IActionResult> MyPayments([FromQuery]QueryParameters param)
        {
            var studentId = CurrentUserId;
            
            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _tuitionService.GetPaymentsForStudentAsync(studentId.Value,param);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            var payments = result.Data;

            var dto = _mapper.Map<List<TuitionPaymentDto>>(payments);

            return Ok(dto);
        }
        [HttpPost("MockPayment")]
        public async Task<IActionResult> MockPayment([FromBody]PaymentParameters param)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _tuitionService.MockPayment(param,studentId.Value);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok(result.Message);
        }
    }
}
