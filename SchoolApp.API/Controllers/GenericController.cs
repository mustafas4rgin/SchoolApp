using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Application.Concrete;
using SchoolApp.Application.DTOs;
using SchoolApp.Domain.Entities;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TListDto, TCreateDto, TUpdateDto> : BaseApiController
    where T : EntityBase
    where TListDto : class
    where TCreateDto : class
    where TUpdateDto : class
    {
        private readonly IGenericService<T> _genericService;
        private readonly IValidator<TCreateDto> _createValidator;
        private readonly IValidator<TUpdateDto> _updateValidator;
        public readonly IMapper _mapper;
        public GenericController(
            IGenericService<T> genericService,
            IValidator<TCreateDto> createValidator,
            IValidator<TUpdateDto> updateValidator,
            IMapper mapper
        )
        {
            _genericService = genericService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public virtual async Task<IActionResult> GetAll([FromQuery] QueryParameters param)
        {
            var result = await _genericService.GetAllAsync();

            var errorResponse = HandleServiceResult(result);

            if (errorResponse != null)
                return errorResponse;

            var entities = result.Data;

            var dto = _mapper.Map<List<TListDto>>(entities);

            return Ok(dto);
        }
        [HttpGet("GetById/{id}")]
        public virtual async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] QueryParameters param)
        {
            var result = await _genericService.GetByIdAsync(id);

            var errorResponse = HandleServiceResult(result);

            if (errorResponse != null)
                return errorResponse;

            var entity = result.Data;

            var dto = _mapper.Map<TListDto>(entity);

            return Ok(dto);
        }
        [HttpPost("Add")]
        public virtual async Task<IActionResult> Add([FromBody]TCreateDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return HandleValidationErrors(validationResult.Errors);

        var entity = _mapper.Map<T>(dto);

        var addingResult = await _genericService.AddAsync(entity);

        var errorResponse = HandleServiceResult(addingResult);

            if (errorResponse != null)
                return errorResponse;

            return Ok(addingResult);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody]TUpdateDto dto, [FromRoute]int id)
        {
            var existingEntityResult = await _genericService.GetByIdAsync(id);

            var existingEntityErrorResponse = HandleServiceResult(existingEntityResult);

            if (existingEntityErrorResponse != null)
                return existingEntityErrorResponse;

            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return HandleValidationErrors(validationResult.Errors);

            var existingEntity = existingEntityResult.Data;

            _mapper.Map(dto, existingEntity);

            var updatingResult = await _genericService.UpdateAsync(existingEntity);

            var updatingEntityErrorResponse = HandleServiceResult(updatingResult);

            if (updatingEntityErrorResponse != null)
                return updatingEntityErrorResponse;

            return Ok(updatingResult);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _genericService.DeleteAsync(id);

            var errorResponse = HandleServiceResult(result);

            if (errorResponse != null)
                return errorResponse;

            return Ok(result);
        }
        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> Restore([FromRoute]int id)
        {
            var result = await _genericService.RestoreAsync(id);

            var errorResponse = HandleServiceResult(result);

            if (errorResponse != null)
                return errorResponse;

            return Ok(result);
        }
        [HttpGet("GetAllDeleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var result = await _genericService.GetAllDeleted();

            var errorResponse = HandleServiceResult(result);

            if (errorResponse != null)
                return errorResponse;

            var deletedEntities = result.Data;

            var dto = _mapper.Map<List<TListDto>>(deletedEntities);

            return Ok(dto);
        }
    }
}
