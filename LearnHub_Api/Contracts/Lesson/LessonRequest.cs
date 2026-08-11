namespace LearnHub_Api.Contracts.Lesson;

public record LessonRequest(
    string Title,
    string Description,
    string VideoUrl,
    int DurationInMinutes,
    int Order
);