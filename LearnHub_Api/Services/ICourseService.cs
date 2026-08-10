using LearnHub_Api.Contracts.Course;

namespace LearnHub_Api.Services
{
    public interface ICourseService
    {
        Task<Result<CourseResponse>> CreateAsync(CourseRequest request, CancellationToken cancellationToken);
        Task<Result<CourseResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
