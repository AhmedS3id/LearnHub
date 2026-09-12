using LearnHub_Api.Authentication.Filter;
using LearnHub_Api.Common;
using LearnHub_Api.Contracts.Review;
using LearnHub_API.Abstractions.Consts;
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
        [HasPermission(Permissions.GetReviews)]
        public async Task<IActionResult> GetAllReviews([FromRoute] int courseId,[FromQuery] RequestFilter filter,CancellationToken cancellationToken)
        {
            var result = await _reviewService.GetAllReviewsAsync(courseId,filter,cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPut("{reviewId}")]
        [HasPermission(Permissions.UpdateReviews)]
        public async Task<IActionResult> UpdateReviews([FromRoute] int reviewId, [FromBody] ReviewRequest request, CancellationToken cancellationToken)
        {
            var result = await _reviewService.UpdateAsync(reviewId, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("{reviewId}")]
        [HasPermission(Permissions.DeleteReviews)]
        public async Task<IActionResult> Delete([FromRoute] int reviewId, CancellationToken cancellationToken)
        {
            var result = await _reviewService.DeleteAsync(reviewId, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
