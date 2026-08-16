using LearnHub_Api.Contracts.Enrollment;
using LearnHub_Api.Contracts.Section;
using LearnHub_Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearnHub_Api.Services
{
    public class EnrollmentServices (ApplicationDbContext context,IHttpContextAccessor httpContextAccessor) : IEnrollmentService
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

            await _context.Enrollments.AddAsync(
                enrollment,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = enrollment.Adapt<EnrollmentResponse>();

            return Result.Success(response);
        }
    }
}
