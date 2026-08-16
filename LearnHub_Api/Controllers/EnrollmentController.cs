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
        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId,CancellationToken cancellationToken)
        {
            var result = await _enrollment.CreateAsync(courseId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _enrollment.GetMyEnrollmentsAsync( cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPut("{enrollmentId}")]
        public async Task<IActionResult> GetById([FromRoute]int enrollmentId,CancellationToken cancellationToken)
        {
            var result = await _enrollment.GetByIdAsync(enrollmentId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
