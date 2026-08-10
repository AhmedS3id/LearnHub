namespace LearnHub_Api.Errors;

public static class CategoryErrors
{
    public static readonly Error AlreadyExists =
        new(
            "Category.AlreadyExists",
            "Category already exists.",
            StatusCodes.Status409Conflict
        );
    public static readonly Error NotFound =
    new(
        "Category.NotFound",
        "Category not found.",
        StatusCodes.Status404NotFound
    );
    public static readonly Error DuplicatedName =
     new(
         "Category.DuplicatedName",
         "Category name already exists.",
         StatusCodes.Status409Conflict
     );
}