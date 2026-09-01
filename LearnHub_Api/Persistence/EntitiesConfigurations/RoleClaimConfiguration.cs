using LearnHub_Api.Abstractions.Consts;
using LearnHub_API.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RoleClaimConfiguration
    : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(
        EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var claims = new List<IdentityRoleClaim<string>>();

        var adminPermissions = Permissions.GetAllPermissions();

        AddClaims(claims,DefaultRoles.AdminRoleId,adminPermissions);

        var instructorPermissions = new List<string>
        {
            Permissions.GetCourses,
            Permissions.AddCourses,
            Permissions.UpdateCourses,
            Permissions.DeleteCourses,
            Permissions.GetSections,
            Permissions.AddSections,
            Permissions.UpdateSections,
            Permissions.DeleteSections,
            Permissions.GetLessons,
            Permissions.AddLessons,
            Permissions.UpdateLessons,
            Permissions.DeleteLessons,
            Permissions.GetCategories,
            Permissions.GetReviews,
            Permissions.GetProfile,
            Permissions.UpdateProfile,
            Permissions.GetEnrollments,
            Permissions.UpdateEnrollments
        };

        AddClaims(
            claims,
            DefaultRoles.InstructorRoleId,
            instructorPermissions);

        var memberPermissions = new List<string>
        {
            Permissions.GetCategories,

            Permissions.GetCourses,

            Permissions.GetEnrollments,
            Permissions.AddEnrollments,

            Permissions.GetSections,
            Permissions.GetLessons,

            Permissions.GetReviews,
            Permissions.AddReviews,
            Permissions.UpdateReviews,
            Permissions.DeleteReviews,

            Permissions.GetProfile,
            Permissions.UpdateProfile
        };

        AddClaims(
            claims,
            DefaultRoles.MemberRoleId,
            memberPermissions);

        builder.HasData(claims);
    }

    private static void AddClaims(
        List<IdentityRoleClaim<string>> claims,
        string roleId,
        IEnumerable<string> permissions)
    {
        foreach (var permission in permissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = claims.Count + 1,
                RoleId = roleId,
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }
    }
}