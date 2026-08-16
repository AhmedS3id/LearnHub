using LearnHub_Api.Contracts.Review;

namespace LearnHub_Api.Services
{
    public interface IReviewService
    {
        Task<Result<ReviewResponse>> CreateAsync(int courseId,ReviewRequest request,CancellationToken cancellationToken);
        Task<Result<IEnumerable<ReviewResponse>>> GetAllReviewsAsync(int courseId, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int reviewId,ReviewRequest request, CancellationToken cancellationToken);
    }
}
