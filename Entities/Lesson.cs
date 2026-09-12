namespace LearnHub_Api.Entities
{
    public class Lesson : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty; 
        public int DurationInMinutes { get; set; }
        public int Order { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}
