using LearnHub_Api.Contracts.Enrollment;
using LearnHub_Api.Contracts.Section;
using LearnHub_Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearnHub_Api.Services
{
    public class EnrollmentServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IEnrollmentService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<EnrollmentResponse>> CreateAsync(int courseId,CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .FindAsync([courseId], cancellationToken);

            if (course is null)
                return Result.Failure<EnrollmentResponse>(
                    CourseErrors.NotFound);

            var studentId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();

            var enrollmentExists = await _context.Enrollments
                .AnyAsync(
                x => x.StudentId == studentId &&
                x.CourseId == courseId,cancellationToken);

            if (enrollmentExists)
                return Result.Failure<EnrollmentResponse>(
                    EnrollmentErrors.AlreadyEnrolled);

            var enrollment = new Enrollment
            {
                StudentId = studentId!,
                CourseId = courseId,
                Progress = 0
            };

            await _context.Enrollments.AddAsync(enrollment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = enrollment.Adapt<EnrollmentResponse>();

            return Result.Success(response);
        }
        public async Task<Result<IEnumerable<EnrollmentResponse>>> GetMyEnrollmentsAsync(CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext!.User.GetUserId();

            var enrollments = await _context.Enrollments
                .AsNoTracking()
                .Where(x => x.StudentId == studentId)
                .Select(x => new EnrollmentResponse(
                    x.Id,
                    x.CourseId,
                    x.Course.Title,
                    x.Progress,
                    x.EnrolledOn
                )).ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<EnrollmentResponse>>(enrollments);
        }
        public async Task<Result<EnrollmentResponse>> GetByIdAsync(int enrollmentId,CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext!
                .User
                .GetUserId();

            var response = await _context.Enrollments
                .AsNoTracking()
                .Where(x => x.Id == enrollmentId 
                &&x.StudentId == studentId)
                .Select(x => new EnrollmentResponse(
                    x.Id,
                    x.CourseId,
                    x.Course.Title,
                    x.Progress,
                    x.EnrolledOn
                ))
                .SingleOrDefaultAsync(cancellationToken);

            if (response is null)
                return Result.Failure<EnrollmentResponse>(
                    EnrollmentErrors.NotFound);

            return Result.Success(response);
        }

    }
}
