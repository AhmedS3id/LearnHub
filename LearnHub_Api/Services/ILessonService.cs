using LearnHub_Api.Contracts.Lesson;

namespace LearnHub_Api.Services
{
    public interface ILessonService
    {
        Task<Result<LessonResponse>> CreateAsync( int courseId, LessonRequest request,CancellationToken cancellationToken);
        Task<Result<LessonResponse>> GetByIdAsync( int courseId,CancellationToken cancellationToken);
    }
}
