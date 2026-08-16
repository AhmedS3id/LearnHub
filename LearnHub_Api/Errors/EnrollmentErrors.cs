namespace LearnHub_Api.Errors
{
    public static class EnrollmentErrors
    {
        public static readonly Error AlreadyEnrolled =
            new("Enrollment.AlreadyEnrolled",
                "You are already enrolled in this course.",
                StatusCodes.Status409Conflict);
    }
}