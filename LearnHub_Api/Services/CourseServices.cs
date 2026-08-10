using LearnHub_Api.Contracts.Course;
using LearnHub_Api.Errors;
using LearnHub_Api.Extensions;
namespace LearnHub_Api.Services
{
    public class CourseServices(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor) : ICourseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<CourseResponse>> CreateAsync(CourseRequest request,CancellationToken cancellationToken)
        {
            var category = await _context.Categories
               .FindAsync([request.CategoryId], cancellationToken);
            if (category is null)
                return Result.Failure<CourseResponse>(CourseErrors.CategoryNotFound);

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
    }
}
