namespace LearnHub_Api.Contracts.Review
{
    public record ReviewResponse(
        int Id,
        string StudentName,
        string CourseName,
        string Comment,
        int Rating,
        DateTime CreatedOn
        );
}
