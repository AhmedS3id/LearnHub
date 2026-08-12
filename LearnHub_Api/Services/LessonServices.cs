using LearnHub_Api.Contracts.Lesson;
using LearnHub_Api.Extensions;
using Microsoft.AspNetCore.Identity;

namespace LearnHub_Api.Services
{
    public class LessonServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : ILessonService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        //         if (await _userManager.FindByIdAsync(id) is not { }user)
        public async Task<Result<LessonResponse>> CreateAsync(int courseId,LessonRequest request,CancellationToken cancellationToken)
        {
            // var courseIsExist = await _context.Courses.FindAsync([Id], cancellationToken); 

            if (await _context.Courses.FindAsync([courseId],cancellationToken) is not { } course)
                return Result.Failure<LessonResponse>(CourseErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext?.User.GetUserId();
            if (course.InstructorId != instructorId)
                return Result.Failure<LessonResponse>(LessonErrors.Unauthorized);

            var lesson = request.Adapt<Lesson>();
            lesson.CourseId = courseId;

            await _context.Lessons.AddAsync(
            lesson, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = new LessonResponse(
                lesson.Id,
                lesson.Title,
                lesson.Description,
                lesson.VideoUrl,
                lesson.DurationInMinutes,
                lesson.Order,
                course.Title);
            return Result.Success(response);
        }
    }
}
