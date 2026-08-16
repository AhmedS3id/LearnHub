using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentController(IEnrollmentService enrollment) : ControllerBase
    {
        private readonly IEnrollmentService _enrollment = enrollment;
        [HttpPost("{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId,CancellationToken cancellationToken)
        {
            var result = await _enrollment.CreateAsync(courseId, cancellationToken);
            return result.IsSuccess ? Ok(result) : result.ToProblem();
        }
    }
}
