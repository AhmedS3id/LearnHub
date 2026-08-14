using LearnHub_Api.Contracts.Section;
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
                && x.Order==request.Order,
                cancellationToken);
            if (sectionOrderExists)
                return Result.Failure<SectionResponse>(
                    SectionErrors.DuplicatedOrder);

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
            var course = await _context.Courses
              .AnyAsync(x => x.Id == courseId, cancellationToken);
            if (!course )
                return Result.Failure<IEnumerable<SectionResponse>>(CourseErrors.NotFound);

            var response = await _context.Sections
                .AsNoTracking()
                .Where(x=>x.CourseId == courseId)
                .OrderBy(x=>x.Order)
                .ProjectToType<SectionResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<SectionResponse>>(response);
        }
    }
}
