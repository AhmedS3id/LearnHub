namespace LearnHub_Api.Errors;

public static class LessonErrors
{
    public static readonly Error NotFound =
        new(
            "Lesson.NotFound",
            "Lesson not found.",
            StatusCodes.Status404NotFound
        );

    public static readonly Error CourseNotFound =
        new(
            "Lesson.CourseNotFound",
            "Course not found.",
            StatusCodes.Status404NotFound
        );

    public static readonly Error Unauthorized =
        new(
            "Lesson.Unauthorized",
            "You are not authorized to modify this lesson.",
            StatusCodes.Status403Forbidden
        );
}