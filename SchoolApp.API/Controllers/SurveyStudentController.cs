using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Controllers;
using SchoolApp.Application.Concrete;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyStudentController : BaseApiController
    {
        private readonly ISurveyStudentService _surveyStudentService;
        public SurveyStudentController(ISurveyStudentService surveyStudentService)
        {
            _surveyStudentService = surveyStudentService;
        }
        [HttpPut("MarkAsAnswered/{surveyId}")]
        public async Task<IActionResult> MarkAsAnswered([FromRoute]int surveyId)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _surveyStudentService.MarkAsAnswered(studentId.Value,surveyId);

            var errorResult = HandleServiceResult(result);

            if (errorResult != null)
                return errorResult;

            return Ok(result.Message);
        }
        [HttpGet("HasAnswered/{surveyId}")]
        public async Task<IActionResult> HasAnswered([FromRoute]int surveyId)
        {
            var studentId = CurrentUserId;

            if (studentId is null)
                return Unauthorized("Auth error.");

            var result = await _surveyStudentService.HasStudentAnsweredSurvey(studentId.Value,surveyId);

            return Ok(new { hasAnswered = result.Success });
        }
    }
}
