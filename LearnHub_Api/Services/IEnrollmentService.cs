using LearnHub_Api.Contracts.Enrollment;

namespace LearnHub_Api.Services
{ 
    public interface IEnrollmentService
    {
        Task<Result<EnrollmentResponse>> CreateAsync(
            int courseId,
            CancellationToken cancellationToken);

        //Task<Result<IEnumerable<EnrollmentResponse>>> GetMyEnrollmentsAsync(
        //    CancellationToken cancellationToken);

        //Task<Result<EnrollmentResponse>> GetByIdAsync(
        //    int enrollmentId,
        //    CancellationToken cancellationToken);
    }
}
