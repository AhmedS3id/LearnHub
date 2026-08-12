using LearnHub_Api.Contracts.Lesson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonController(ILessonService lessonService) : ControllerBase
    {
        private readonly ILessonService _lessonService = lessonService;

        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] LessonRequest request,CancellationToken cancellationToken)
        {
            var result = await _lessonService.CreateAsync(courseId, request,cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { lessonId = result.Value.Id }
            ,result.Value)
                : result.ToProblem();
        }
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int courseId, CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetAllAsync( courseId,cancellationToken);
            return result.IsSuccess? Ok(result.Value):result.ToProblem();
        }
        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetById([FromRoute] int lessonId,CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetByIdAsync(lessonId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
