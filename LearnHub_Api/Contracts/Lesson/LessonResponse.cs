namespace LearnHub_Api.Contracts.Lesson;
public record LessonResponse(
    int Id,
    string Title,
    string Description,
    string VideoUrl,
    int DurationInMinutes,
    int Order,
    string CourseTitle
);