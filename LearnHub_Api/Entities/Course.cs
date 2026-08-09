namespace LearnHub_Api.Entities
{
    public class Course:AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public decimal Price { get; set; }
        public int CategoryId {  get; set; }
        public string InstructorId { get; set; } = string.Empty;

        public Category Category { get; set; } = null!;
        public ApplicationUser Instructor { get; set; } = null!;
        public ICollection<Lesson> Lessons { get; set; } = [];
        
    }
}
