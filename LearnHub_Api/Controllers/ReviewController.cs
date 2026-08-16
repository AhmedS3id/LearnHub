using LearnHub_Api.Contracts.Review;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] ReviewRequest request,CancellationToken cancellationToken)
        {
            var result = await _reviewService.CreateAsync(courseId,request,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAllReviews([FromRoute] int courseId,CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetAllReviewsAsync(courseId,cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
    }
}
