namespace LearnHub_Api.Errors;

public static class CategoryErrors
{
    public static readonly Error AlreadyExists =
        new(
            "Category.AlreadyExists",
            "Category already exists.",
            StatusCodes.Status409Conflict
        );
}