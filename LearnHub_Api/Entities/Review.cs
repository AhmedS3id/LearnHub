namespace LearnHub_Api.Entities
{
    public class Review : AuditableEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string StudentId {  get; set; }= string.Empty;
        public int CourseId {  get; set; }

        public ApplicationUser Student { get; set; } = null!;
        public Course Course { get; set; }= null!;
    }
}
