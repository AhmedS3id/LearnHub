using LearnHub_Api.Contracts.Lesson;

namespace LearnHub_Api.Services
{
    public interface ILessonService
    {
        Task<Result<LessonResponse>> CreateAsync(int sectionId, LessonRequest request, CancellationToken cancellationToken);
        Task<Result<LessonResponse>> GetByIdAsync(int sectionId,int lessonId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<SectionWithLessonsResponse>>> GetCourseContentAsync(int courseId, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int sectionId, int lessonId, LessonRequest request, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int sectionId, int lessonId, CancellationToken cancellationToken);
    }
}
