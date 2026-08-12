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
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
    }
}
