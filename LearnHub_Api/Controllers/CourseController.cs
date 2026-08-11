using LearnHub_Api.Contracts.Course;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        private readonly ICourseService _courseService = courseService;

        [HttpPost("")]
        public async Task< IActionResult> Create([FromBody]CourseRequest request,CancellationToken cancellationToken)
        {
            var result = await _courseService.CreateAsync(request, cancellationToken);
            return result.IsSuccess? CreatedAtAction(nameof (GetById), new { id = result.Value.Id }, result.Value) :result.ToProblem();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
        {
            var result = await _courseService.GetAllAsync( cancellationToken);
            return Ok(result);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory([FromRoute]int id,CancellationToken cancellationToken)
        {
            var result = await _courseService.GetByCategoryAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("{id}")]
        public async Task< IActionResult> GetById([FromRoute] int id ,CancellationToken cancellationToken)
        {
            var result = await _courseService.GetByIdAsync(id, cancellationToken);
            return result.IsSuccess? Ok(result.Value):result.ToProblem();
        }
        [HttpPut("{courseId}")]
        public async Task< IActionResult> Update([FromRoute] int courseId ,CourseRequest request,CancellationToken cancellationToken)
        {
            var result = await _courseService.UpdateAsync(courseId, request, cancellationToken);

            return result.IsSuccess? NoContent():result.ToProblem();
        }
        [HttpDelete("{courseId}")]

        public async Task< IActionResult> Delete([FromRoute] int courseId ,CancellationToken cancellationToken)
        {
            var result = await _courseService.DeleteAsync(courseId, cancellationToken);

            return result.IsSuccess? NoContent():result.ToProblem();
        }
    }
}
