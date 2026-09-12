using LearnHub_Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub_Api.Persistence.EntitiesConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name)
           .HasMaxLength(100)
           .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(100);
        }
    }
}
