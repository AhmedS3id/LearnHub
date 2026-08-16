namespace LearnHub_Api.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int Progress { get; set; }
        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;

        public Course Course { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
    }
}