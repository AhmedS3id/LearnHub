using LearnHub_Api.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub_Api.Persistence.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
               .OwnsMany(x => x.RefreshTokens)
               .ToTable("RefreshTokens")
               .WithOwner()
               .HasForeignKey("UserId");

            builder.HasData(new ApplicationUser
            {
                Id = DefaultUsers.AdminId,
                FirstName = "LearnHub",
                LastName = "Admin",
                UserName = DefaultUsers.AdminEmail,
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                EmailConfirmed = true,
                PasswordHash = DefaultUsers.AdminPasswordHash
            });
        }
    }
}
