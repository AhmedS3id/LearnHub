using LearnHub_Api.Contracts.Course;
using LearnHub_Api.Errors;
using LearnHub_Api.Extensions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
namespace LearnHub_Api.Services
{
    public class CourseServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : ICourseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

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

        public async Task<IEnumerable<CourseResponse>> GetAllAsync(CancellationToken cancellationToken)=>
            await _context.Courses
            .AsNoTracking()
            .ProjectToType<CourseResponse>()
            .ToListAsync(cancellationToken);

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
    }
}
