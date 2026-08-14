using LearnHub_Api.Contracts.Lesson;
using LearnHub_Api.Entities;
using LearnHub_Api.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;
using static System.Collections.Specialized.BitVector32;

namespace LearnHub_Api.Services
{
    public class LessonServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : ILessonService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task<Result<LessonResponse>> CreateAsync(int sectionId, LessonRequest request, CancellationToken cancellationToken)
        {
            var section = await _context.Sections
                .Include(x => x.Course)
                .FirstOrDefaultAsync(
                    x => x.Id == sectionId,
                    cancellationToken);

            if (section is null)
                return Result.Failure<LessonResponse>(
                    SectionErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();

            if (section.Course.InstructorId != instructorId)
                return Result.Failure<LessonResponse>(
                    LessonErrors.Unauthorized);

            var lessonExists = await _context.Lessons
                .AnyAsync(
                    x => x.SectionId == sectionId &&
                         x.Order == request.Order,
                    cancellationToken);

            if (lessonExists)
                return Result.Failure<LessonResponse>(
                    LessonErrors.DuplicatedOrder);

            var lesson = request.Adapt<Lesson>();

            lesson.SectionId = sectionId;

            await _context.Lessons.AddAsync(
                lesson,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = lesson.Adapt<LessonResponse>();

            return Result.Success(response);
        }
        public async Task<Result<IEnumerable<SectionWithLessonsResponse>>> GetCourseContentAsync(int courseId, CancellationToken cancellationToken)
        {
            var courseExists = await _context.Courses
                .AnyAsync(x => x.Id == courseId, cancellationToken);
            if (!courseExists)
                return Result.Failure<IEnumerable<SectionWithLessonsResponse>>(CourseErrors.NotFound);

            var response = await _context.Sections
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.Order)
                .Select(s => new SectionWithLessonsResponse(
                    s.Id,
                    s.Title,
                    s.Order,
                    s.Lessons.OrderBy(l => l.Order).Select(l => new LessonResponse(
                        l.Id, l.Title, l.Description, l.VideoUrl, l.DurationInMinutes, l.Order
                    ))
                ))
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<SectionWithLessonsResponse>>(response);
        }
        public async Task<Result<LessonResponse>> GetByIdAsync(int sectionId, int lessonId, CancellationToken cancellationToken)
        {
            var section = await _context.Sections
                .AnyAsync(x => x.Id == sectionId,cancellationToken);
            if (!section)
                return Result.Failure<LessonResponse>(
                    SectionErrors.NotFound);

            var response = await _context.Lessons
                .AsNoTracking()
                .Where(x => x.Id == lessonId && x.SectionId == sectionId)
                .ProjectToType<LessonResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
                return Result.Failure<LessonResponse>(
                    LessonErrors.NotFound);

            return Result.Success(response);
        }

        public async Task<Result> UpdateAsync(int sectionId, int lessonId, LessonRequest request, CancellationToken cancellationToken)
        {
            //var lesson = await _context.Lessons
            //  .Include(x => x.Section)
            //  .ThenInclude(x => x.Course)
            //  .FirstOrDefaultAsync(
            //      x => x.Id == lessonId &&
            //           x.SectionId == sectionId,
            //      cancellationToken);
            //var lesson = await _context.Lessons
            //    .Include(x=>x.Section)
            //    .FirstOrDefaultAsync(x => x.Id == lessonId
            //    && x.SectionId == sectionId, cancellationToken);

            var result = await _context.Lessons
                .Where(x => x.Id == lessonId && x.SectionId == sectionId)
                .Select(x => new
                {
                    Lesson = x,
                    InstructorId = x.Section.Course.InstructorId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
                return Result.Failure(LessonErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!.User.GetUserId();
            if (result.InstructorId != instructorId)
                return Result.Failure(LessonErrors.Unauthorized);

            var orderExists = await _context.Lessons.AnyAsync(
                x => x.SectionId == sectionId
                     && x.Order == request.Order
                     && x.Id != lessonId, cancellationToken);
            if (orderExists)
                return Result.Failure(LessonErrors.DuplicatedOrder);

            var lesson = result.Lesson;
            lesson.Title = request.Title;
            lesson.Description = request.Description;
            lesson.VideoUrl = request.VideoUrl;
            lesson.DurationInMinutes = request.DurationInMinutes;
            lesson.Order = request.Order;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int sectionId, int lessonId, CancellationToken cancellationToken)
        {

            var result = await _context.Lessons
              .Where(x => x.Id == lessonId && x.SectionId == sectionId)
              .Select(x => new
              {
                  Lesson = x,
                  InstructorId = x.Section.Course.InstructorId
              })
              .FirstOrDefaultAsync(cancellationToken);

            if (result is null)
                return Result.Failure(LessonErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!.User.GetUserId();
            if (result.InstructorId != instructorId)
                return Result.Failure(LessonErrors.Unauthorized);

            _context.Lessons.Remove(result.Lesson);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
