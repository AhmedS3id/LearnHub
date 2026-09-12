namespace LearnHub_Api.Contracts.Section
{
    public record SectionResponse(
        int Id,
        string Title,
        int Order,
        string CourseTitle
        );
}
