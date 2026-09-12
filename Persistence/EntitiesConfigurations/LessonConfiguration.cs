using LearnHub_Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub_Api.Persistence.EntitiesConfigurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(200)
                  .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.VideoUrl)
                .HasMaxLength(500);

            builder.HasOne(x => x.Section)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
