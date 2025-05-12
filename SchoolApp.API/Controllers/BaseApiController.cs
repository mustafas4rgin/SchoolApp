using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Domain.Contracts;
using SchoolApp.Domain.Results;

namespace SchoolApp.API.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected int? CurrentUserId => User.GetUserId();
        protected IActionResult? HandleServiceResult<T>(IServiceResultWithData<T> result) where T : class
        {
            if (!result.Success)
                return NotFound(new { result.Message });

            return null;
        }

        protected IActionResult? HandleServiceResult(IServiceResult result)
        {
            if (!result.Success)
                return BadRequest(new { result.Message });

            return null;
        }

        protected IActionResult HandleValidationErrors(IEnumerable<ValidationFailure> errors)
        {
            var errorResponse = errors.Select(e => new
            {
                Property = e.PropertyName,
                Error = e.ErrorMessage
            });

            return BadRequest(errorResponse);
        }

    }
}
