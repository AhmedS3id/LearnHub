namespace LearnHub_Api.Errors
{
    public static class ReviewErrors
    {
        public static readonly Error NotFound =
            new("Review.NotFound", "Review not found.", StatusCodes.Status404NotFound);

        public static readonly Error Unauthorized =
            new("Review.Unauthorized", "You are not authorized to perform this action.", StatusCodes.Status403Forbidden);

        public static readonly Error NotEnrolled =
            new("Review.NotEnrolled", "You must be enrolled in the course to add a review.", StatusCodes.Status403Forbidden);

        public static readonly Error AlreadyReviewed =
            new("Review.AlreadyReviewed", "You have already reviewed this course.", StatusCodes.Status409Conflict);
    }
}
