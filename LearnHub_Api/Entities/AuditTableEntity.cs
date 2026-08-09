namespace LearnHub_Api.Entities
{
    public abstract class AuditableEntity
    {
        public string CreatedById { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string? UpdatedById { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; } = null!;

        public ApplicationUser? UpdatedBy { get; set; }
    }
}