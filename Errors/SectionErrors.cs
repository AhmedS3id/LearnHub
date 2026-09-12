namespace LearnHub_Api.Errors
{
    public static class SectionErrors
    {
        public static readonly Error NotFound =
            new(
                "Section.NotFound",
                "Section not found.",
                StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedOrder =
            new(
                "Section.DuplicatedOrder",
                "Another section with the same order already exists in this course.",
                StatusCodes.Status409Conflict);

        public static readonly Error Unauthorized =
            new(
                "Section.Unauthorized",
                "You are not authorized to modify this section.",
                StatusCodes.Status403Forbidden);
    }
}