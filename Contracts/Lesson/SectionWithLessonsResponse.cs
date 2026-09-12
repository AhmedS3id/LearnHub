namespace LearnHub_Api.Contracts.Lesson;
public record SectionWithLessonsResponse(
    int Id,
    string Title,
    int Order,
    IEnumerable<LessonResponse> Lessons
);