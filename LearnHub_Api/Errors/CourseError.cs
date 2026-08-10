namespace LearnHub_Api.Errors;

public static class CourseErrors
{
    public static readonly Error NotFound =
        new(
            "Course.NotFound",
            "Course not found.",
            StatusCodes.Status404NotFound
        );
    public static readonly Error InstructorNotFound =
        new(
            "Instructor.NotFound",
            "Instructor Not Found.",
            StatusCodes.Status404NotFound
        );

    public static readonly Error CategoryNotFound =
        new(
            "Course.CategoryNotFound",
            "Category not found.",
            StatusCodes.Status404NotFound
        );
}