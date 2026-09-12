namespace LearnHub_Api.Entities;

public class Section : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Order { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public ICollection<Lesson> Lessons { get; set; } = [];
}