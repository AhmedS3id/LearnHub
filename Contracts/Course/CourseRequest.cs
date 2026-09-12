namespace LearnHub_Api.Contracts.Course
{
    public record CourseRequest(
      string Title,
      string Description,
      decimal Price,
      int CategoryId
  );
}
