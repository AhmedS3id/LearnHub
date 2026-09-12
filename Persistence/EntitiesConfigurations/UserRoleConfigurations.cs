using LearnHub_Api.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearnHub_Api.Persistence.EntitiesConfigurations;

public class UserRoleConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{
		//Define Data
		builder.HasData(new IdentityUserRole<string>
		{
			UserId = DefaultUsers.AdminId,
			RoleId = DefaultRoles.AdminRoleId
		}
		);
	}
}