using LearnHub_Api.Contracts.Lesson;
using LearnHub_Api.Entities;

namespace LearnHub_Api.Services
{
    public interface ILessonService
    {
        Task<Result<LessonResponse>> CreateAsync( int courseId, LessonRequest request,CancellationToken cancellationToken);
        Task<Result<LessonResponse>> GetByIdAsync( int courseId,CancellationToken cancellationToken);
        Task<Result<IEnumerable<LessonResponse>>> GetAllAsync(int courseId, CancellationToken cancellationToken);
       // Task<Result> UpdateAsync( int courseId,LessonRequest request,CancellationToken cancellationToken);
    }
}
