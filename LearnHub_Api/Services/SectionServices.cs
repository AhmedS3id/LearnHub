using LearnHub_Api.Contracts.Section;
using LearnHub_Api.Entities;
using LearnHub_Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearnHub_Api.Services
{
    public class SectionServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : ISectionService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<SectionResponse>> CreateAsync(int courseId, SectionRequest request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
               .FindAsync([courseId], cancellationToken);
            if (course is null)
                return Result.Failure<SectionResponse>(CourseErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();

            if (course.InstructorId != instructorId)
                return Result.Failure<SectionResponse>(
                    SectionErrors.Unauthorized);

            var sectionOrderExists = await _context.Sections
                .AnyAsync(x =>x.CourseId == courseId
                && x.Order==request.Order, cancellationToken);
            if (sectionOrderExists)
                return Result.Failure<SectionResponse>(SectionErrors.DuplicatedOrder);

            var section = request.Adapt<Section>();
            section.CourseId = courseId;
            await _context.AddAsync(section, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new SectionResponse(
                section.Id,
                section.Title,
                section.Order,
                course.Title);
            return Result.Success(response);

        }
        public async Task<Result<IEnumerable<SectionResponse>>> GetAllAsync(int courseId, CancellationToken cancellationToken)
        {
            var courseExists = await _context.Courses
              .AnyAsync(x => x.Id == courseId, cancellationToken);
            if (!courseExists )
                return Result.Failure<IEnumerable<SectionResponse>>(CourseErrors.NotFound);

            var response = await _context.Sections
                .AsNoTracking()
                .Where(x=>x.CourseId == courseId)
                .OrderBy(x=>x.Order)
                .ProjectToType<SectionResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<SectionResponse>>(response);
        }

        public async Task<Result<SectionResponse>> GetByIdAsync(int courseId, int sectionId, CancellationToken cancellationToken)
        {
            var courseExists = await _context.Courses
                .AnyAsync(x => x.Id == courseId, cancellationToken);
            if (!courseExists)
                return Result.Failure<SectionResponse>(CourseErrors.NotFound);

            var response = await _context.Sections
                .AsNoTracking()
                .Where(x => x.CourseId == courseId && x.Id == sectionId)
                .ProjectToType<SectionResponse>()
                .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
                return Result.Failure<SectionResponse>(SectionErrors.NotFound);

            return Result.Success(response);
        }

        public async Task<Result> UpdateAsync(int courseId, int sectionId, SectionRequest request, CancellationToken cancellationToken)
        {
    //        var section = await _context.Sections
    //          .Include(x => x.Course)
    //          .FirstOrDefaultAsync(x => x.Id == sectionId && x.CourseId == courseId, cancellationToken);
    //        if (section is null)
    //            return Result.Failure(SectionErrors.NotFound);

            var course = await _context.Courses
                .FindAsync([courseId], cancellationToken);
            if (course is null)
                return Result.Failure(CourseErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();
            if (course.InstructorId != instructorId)
                return Result.Failure(SectionErrors.Unauthorized);

            var section = await _context.Sections
                .FirstOrDefaultAsync(x => x.Id == sectionId && x.CourseId == courseId, cancellationToken);
            if (section is null)
                return Result.Failure(SectionErrors.NotFound);

            var sectionOrderExists = await _context.Sections
                .AnyAsync(x => x.CourseId == courseId
                    && x.Order == request.Order
                    && x.Id != section.Id, cancellationToken);
            if (sectionOrderExists)
                return Result.Failure(SectionErrors.DuplicatedOrder);

            section.Title = request.Title;
            section.Order = request.Order;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int courseId, int sectionId, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
              .FindAsync([courseId], cancellationToken);
            if (course is null)
                return Result.Failure(CourseErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();

            if (course.InstructorId != instructorId)
                return Result.Failure(
                    SectionErrors.Unauthorized);

            var section = await _context.Sections
                .FirstOrDefaultAsync(x => x.Id == sectionId && x.CourseId == courseId, cancellationToken);
            if (section is null)
                return Result.Failure(SectionErrors.NotFound);

            _context.Remove(section);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

    }
}
