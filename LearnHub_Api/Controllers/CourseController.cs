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
            return result.IsSuccess? Ok(result.Value):result.ToProblem();
        }
    }
}
