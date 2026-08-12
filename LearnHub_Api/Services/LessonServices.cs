using LearnHub_Api.Contracts.Lesson;
using LearnHub_Api.Entities;
using LearnHub_Api.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

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

            var lessonExists = await _context.Lessons.AnyAsync( x => x.CourseId == courseId 
            && x.Order == request.Order,cancellationToken);
            if (lessonExists)
            return Result.Failure<LessonResponse>(LessonErrors.DuplicatedOrder);

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
        public async Task<Result<IEnumerable<LessonResponse>>> GetAllAsync( int courseId, CancellationToken cancellationToken)
        {
            var courseExists = await _context.Courses
                .AnyAsync(x => x.Id == courseId, cancellationToken);

            if (!courseExists)
                return Result.Failure<IEnumerable<LessonResponse>>(
                    CourseErrors.NotFound);

            var response = await _context.Lessons
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.Order)
                .ProjectToType<LessonResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<LessonResponse>>(response);
        }
        public async Task<Result<LessonResponse>> GetByIdAsync(int lessonId,CancellationToken cancellationToken)
        {
            var response = await _context.Lessons
                .AsNoTracking()
                .Where(x => x.Id == lessonId)
                .ProjectToType<LessonResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
                return Result.Failure<LessonResponse>(
                    LessonErrors.NotFound);

            return Result.Success(response);
        }

        //public Task<Result> UpdateAsync(int courseId, LessonRequest request, CancellationToken cancellationToken)
        //{
        //    if (await _context.Courses.FindAsync([lessonId], cancellationToken) is not { } course)
        //        return Result.Failure<LessonResponse>(LessonErrors.NotFound);
        //}
    }
}
