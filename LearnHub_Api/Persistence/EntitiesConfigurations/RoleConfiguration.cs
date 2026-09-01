using LearnHub_Api.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub_Api.Persistence.EntitiesConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(new ApplicationRole
            {
                Name = DefaultRoles.Admin,
                Id =DefaultRoles.AdminRoleId,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp =DefaultRoles.AdminRoleConcurrencyStamp
                
            });

            builder.HasData(new ApplicationRole
            {
                Name = DefaultRoles.Instructor,
                Id =DefaultRoles.InstructorRoleId,
                NormalizedName = DefaultRoles.Instructor.ToUpper(),
                ConcurrencyStamp =DefaultRoles.InstructorRoleConcurrencyStamp
                
            });

            builder.HasData(new ApplicationRole
            {
                Name = DefaultRoles.Member,
                Id = DefaultRoles.MemberRoleId,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
                IsDefault = true

            });

        }
    }
}
