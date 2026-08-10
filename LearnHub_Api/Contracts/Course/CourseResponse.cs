namespace LearnHub_Api.Contracts.Course;

public record CourseResponse(
    int Id,
    string Title,
    string Description,
    decimal Price,
    string CategoryName,
    string InstructorName
);