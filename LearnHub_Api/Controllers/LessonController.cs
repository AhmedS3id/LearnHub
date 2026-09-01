using LearnHub_Api.Authentication.Filter;
using LearnHub_Api.Contracts.Lesson;
using LearnHub_Api.Entities;
using LearnHub_API.Abstractions.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class LessonController(ILessonService lessonService) : ControllerBase
    {
        private readonly ILessonService _lessonService = lessonService;

        [HttpPost("section/{sectionId}")]
        [HasPermission(Permissions.AddLessons)]
        public async Task<IActionResult> Create([FromRoute] int sectionId,[FromBody] LessonRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _lessonService.CreateAsync(sectionId, request, cancellationToken);
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById),
                    new {lessonId = result.Value.Id },
                    result.Value)
                : result.ToProblem();
        }

        [HttpGet("course/{courseId}")]
        [HasPermission(Permissions.GetLessons)]
        public async Task<IActionResult> GetCourseContent([FromRoute] int courseId,CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetCourseContentAsync(courseId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("course/{courseId}/lesson/{lessonId}")]
        [HasPermission(Permissions.GetLessons)]
        public async Task<IActionResult> GetById([FromRoute] int courseId,[FromRoute] int lessonId,
            CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetByIdAsync(courseId, lessonId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("course/{courseId}/lesson/{lessonId}")]
        [HasPermission(Permissions.UpdateLessons)]
        public async Task<IActionResult> Update([FromRoute] int courseId, [FromRoute] int lessonId,
            [FromBody] LessonRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _lessonService.UpdateAsync(courseId, lessonId, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("course/{courseId}/lesson/{lessonId}")]
        [HasPermission(Permissions.DeleteLessons)]
        public async Task<IActionResult> Delete([FromRoute] int courseId,[FromRoute] int lessonId,
            CancellationToken cancellationToken)
        {
            var result = await _lessonService.DeleteAsync(courseId, lessonId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}