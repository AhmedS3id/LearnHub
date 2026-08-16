namespace LearnHub_Api.Contracts.Enrollment;
public record EnrollmentResponse(
    int Id,
    int CourseId,
    string CourseTitle,
    int Progress,
    DateTime EnrolledOn
);