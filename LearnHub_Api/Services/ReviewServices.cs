using LearnHub_Api.Abstractions;
using LearnHub_Api.Common;
using LearnHub_Api.Contracts.Review;
using LearnHub_Api.Entities;
using LearnHub_Api.Extensions;
using Microsoft.Extensions.Caching.Hybrid;

namespace LearnHub_Api.Services
{
    public class ReviewServices(ApplicationDbContext context
        , HybridCache hybridCache
        , IHttpContextAccessor httpContextAccessor) : IReviewService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly HybridCache _hybridCache = hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<ReviewResponse>> CreateAsync(int courseId, ReviewRequest request, CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext!.User.GetUserId();

            var course = await _context.Courses
                .Where(x => x.Id == courseId)
                .Select(x => new { x.Title })
                .FirstOrDefaultAsync(cancellationToken);

            if (course is null)
                return Result.Failure<ReviewResponse>(CourseErrors.NotFound);

            var isEnrolled = await _context.Enrollments.AnyAsync(x => x.CourseId == courseId 
            && x.StudentId == studentId,cancellationToken);

            if (!isEnrolled)
                return Result.Failure<ReviewResponse>(ReviewErrors.NotEnrolled);

            //var studentName = await _context.Users
            //    .Where(x => x.Id == studentId)
            //    .Select(x => x.FirstName + " " + x.LastName)
            //    .FirstAsync(cancellationToken);

            var review = request.Adapt<Review>();
            review.CourseId = courseId;
            review.StudentId = studentId!;

            await _context.Reviews.AddAsync(review, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _hybridCache.RemoveAsync($"course:{courseId}:reviews",cancellationToken);

            var response = new ReviewResponse(
                review.Id,
                review.Student.FirstName + " " + review.Student.LastName,
                course.Title,
                review.Comment,
                review.Rating,
                review.CreatedOn
            );

            return Result.Success(response);
        }
        public async Task<Result<PaginatedList<ReviewResponse>>> GetAllReviewsAsync(
    int courseId,
    RequestFilter filter,
    CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .Where(x => x.Id == courseId)
                .Select(x => new { x.Title })
                .FirstOrDefaultAsync(cancellationToken);

            if (course is null)
                return Result.Failure<PaginatedList<ReviewResponse>>(
                    CourseErrors.NotFound);

            var query = _context.Reviews
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .Select(x => new ReviewResponse(
                    x.Id,
                    x.Student.FirstName + " " + x.Student.LastName,
                    course.Title,
                    x.Comment,
                    x.Rating,
                    x.CreatedOn
                ));

            var reviews = await PaginatedList<ReviewResponse>.CreateAsync(
                query,
                filter.PageNumber,
                filter.PageSize);

            return Result.Success(reviews);
        }

        public async Task<Result> UpdateAsync(int reviewId, ReviewRequest request, CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext!.User.GetUserId();

            var review = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == reviewId
                &&x.StudentId == studentId, cancellationToken);

            if (review is null)
                return Result.Failure(ReviewErrors.NotFound);

            review.Rating = request.Rating;
            review.Comment = request.Comment;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int reviewId, CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext!.User.GetUserId();

            var review = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == reviewId
                && x.StudentId == studentId, cancellationToken);

            if (review is null)
                return Result.Failure(ReviewErrors.NotFound);

            _context.Remove(review);
            await _context.SaveChangesAsync(cancellationToken);
            await _hybridCache.RemoveAsync($"course:{review.CourseId}:reviews", cancellationToken);
            return Result.Success();
        }

    }
}
