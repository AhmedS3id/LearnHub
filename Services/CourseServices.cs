using LearnHub_Api.Common;
using LearnHub_Api.Contracts.Course;
using LearnHub_Api.Extensions;
using Microsoft.Extensions.Caching.Hybrid;
namespace LearnHub_Api.Services
{
    public class CourseServices(ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        HybridCache hybridCache) : ICourseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly HybridCache _hybridCache = hybridCache;

        public async Task<Result<CourseResponse>> CreateAsync(CourseRequest request,CancellationToken cancellationToken)
        {
            var category = await _context.Categories
               .FindAsync([request.CategoryId], cancellationToken);
            if (category is null)
                return Result.Failure<CourseResponse>(CategoryErrors.NotFound);

            var instructorId = _httpContextAccessor.HttpContext!.User.GetUserId();

            var instructorName = await _context.Users
                .Where(u => u.Id == instructorId)
                .Select(u => u.FirstName + " " + u.LastName)
                .SingleOrDefaultAsync(cancellationToken);

            if (instructorName is null)
                return Result.Failure<CourseResponse>(CourseErrors.InstructorNotFound);

            var course = request.Adapt<Course>();
            course.InstructorId = instructorId!;

            await _context.Courses.AddAsync(course, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _hybridCache.RemoveByTagAsync("courses", cancellationToken);

            var response = new CourseResponse(
                course.Id,
                course.Title,
                course.Description,
                course.Price,
                category.Name,
                instructorName
            );

            return Result.Success(response);
        }

        public async Task<IEnumerable<CourseResponse>> GetAllAsync(RequestFilter filter, CancellationToken cancellationToken)
        {
            var search = filter.SearchValue.Trim();

            var cacheKey = $"courses:search:{search.ToLowerInvariant()}";

            var options = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(5),
                LocalCacheExpiration = TimeSpan.FromMinutes(1)
            };

            return await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cancellationToken =>
                {
                    var query = _context.Courses
                        .AsNoTracking();

                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        query = query.Where(x =>
                            x.Title.Contains(search));
                    }

                    return await query
                        .ProjectToType<CourseResponse>()
                        .ToListAsync(cancellationToken);
                },
                options,
                tags: ["courses"],
                cancellationToken: cancellationToken);
        }

        public async Task<Result<IEnumerable<CourseResponse>>> GetByCategoryAsync(int categoryId,CancellationToken cancellationToken)
        {

            var categoryExists = await _context.Categories
                .AnyAsync(x => x.Id == categoryId, cancellationToken);

            if (!categoryExists)
                return Result.Failure<IEnumerable<CourseResponse>>(
                    CategoryErrors.NotFound);

            var courses = await _context.Courses
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ProjectToType<CourseResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<CourseResponse>>(courses);
        }
        public async Task<Result<CourseResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _context.Courses
                   .AsNoTracking()
                   .Where(x => x.Id == id)
                   .ProjectToType<CourseResponse>()
                   .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
                return Result.Failure<CourseResponse>(
                    CourseErrors.NotFound);

            return Result.Success(response);
        }

        public async Task<Result> UpdateAsync(int courseId, CourseRequest request, CancellationToken cancellationToken)
        {
            var categoryExists = await _context.Categories
               .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!categoryExists)
                return Result.Failure(CategoryErrors.NotFound);

            var course = await _context.Courses
               .FindAsync( [courseId], cancellationToken);

            if (course is null)
                return Result.Failure(CourseErrors.NotFound);

            var user = _httpContextAccessor.HttpContext!.User;
            var instructorId = user.GetUserId();

            if (course.InstructorId != instructorId && !user.IsAdmin())
                return Result.Failure(CourseErrors.Unauthorized);

            course.Title= request.Title;
            course.Description= request.Description;
            course.Price= request.Price;
            course.CategoryId= request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
            await _hybridCache.RemoveByTagAsync("courses", cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int courseId, CancellationToken cancellationToken)
        {
            var course = await _context.Courses
               .FindAsync( [courseId], cancellationToken);

            if (course is null)
                return Result.Failure(CourseErrors.NotFound);

            var user = _httpContextAccessor.HttpContext!.User;
            var instructorId = user.GetUserId();

            if (course.InstructorId != instructorId && !user.IsAdmin())
                return Result.Failure(CourseErrors.Unauthorized);

             _context.Remove(course); 
            await _context.SaveChangesAsync(cancellationToken);
            await _hybridCache.RemoveByTagAsync("courses", cancellationToken);

            return Result.Success();
        }
    }
}
