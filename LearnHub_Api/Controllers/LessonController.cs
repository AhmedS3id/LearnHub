using LearnHub_Api.Contracts.Lesson;
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
        public async Task<IActionResult> Create([FromRoute] int sectionId, [FromBody] LessonRequest request, CancellationToken cancellationToken)
        {
            var result = await _lessonService.CreateAsync(sectionId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    

//        [HttpGet("course/{courseId}")]
//        public async Task<IActionResult> GetAllAsync([FromRoute] int courseId, CancellationToken cancellationToken)
//        {
//            var result = await _lessonService.GetCourseContentAsync(courseId, cancellationToken);
//            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
//        }

        [HttpGet("section/{sectionId}/lesson/{lessonId}")]
        public async Task<IActionResult> GetById([FromRoute] int sectionId, [FromRoute] int lessonId, CancellationToken cancellationToken)
        {
            var result = await _lessonService.GetByIdAsync(sectionId,lessonId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    } 
}
////        [HttpPut("course/{courseId}/lessonId/{lessonId}")]
////        public async Task<IActionResult> Update([FromRoute]int courseId,[FromRoute] int lessonId, [FromBody]LessonRequest request,CancellationToken cancellationToken)
////        {
////            var result = await _lessonService.UpdateAsync(courseId,lessonId, request, cancellationToken);
////            return result.IsSuccess ? NoContent() : result.ToProblem();
////        }
////        [HttpDelete("course/{courseId}/lessonId/{lessonId}")]
////        public async Task<IActionResult> Delete([FromRoute]int courseId,[FromRoute] int lessonId,CancellationToken cancellationToken)
////        {
////            var result = await _lessonService.DeleteAsync(courseId,lessonId, cancellationToken);
////            return result.IsSuccess ? NoContent() : result.ToProblem();
////        }
//    }
//}
