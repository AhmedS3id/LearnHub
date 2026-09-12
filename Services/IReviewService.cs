using LearnHub_Api.Abstractions;
using LearnHub_Api.Common;
using LearnHub_Api.Contracts.Review;

namespace LearnHub_Api.Services
{
    public interface IReviewService
    {
        Task<Result<ReviewResponse>> CreateAsync(int courseId,ReviewRequest request,CancellationToken cancellationToken);
        Task<Result<PaginatedList<ReviewResponse>>> GetAllReviewsAsync(int courseId,RequestFilter filter,CancellationToken cancellationToken); 
        Task<Result> UpdateAsync(int reviewId,ReviewRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int reviewId, CancellationToken cancellationToken);
    }
}
