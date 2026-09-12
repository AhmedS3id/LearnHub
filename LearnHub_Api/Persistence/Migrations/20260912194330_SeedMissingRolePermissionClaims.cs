using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnHub_Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedMissingRolePermissionClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // The "20260911184755_updateIdentityDefaultData" migration deleted the
            // AspNetRoles rows that the original AspNetRoleClaims permission seed
            // referenced. Because IdentityRoleClaim -> Role cascades on delete, that
            // wiped out every role/permission claim before the follow-up UpdateData
            // calls (targeting those now-deleted claim Ids) could re-point them at
            // the current role Ids, leaving AspNetRoleClaims empty. This migration
            // re-seeds the permission claims for the current Admin/Instructor/Member
            // role Ids so [HasPermission] authorization works again.
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 57, "permissions", "users:manage", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 58, "permissions", "categories:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 59, "permissions", "categories:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 60, "permissions", "categories:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 61, "permissions", "categories:delete", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 62, "permissions", "courses:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 63, "permissions", "courses:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 64, "permissions", "courses:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 65, "permissions", "courses:delete", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 66, "permissions", "enrollments:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 67, "permissions", "enrollments:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 68, "permissions", "enrollments:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 69, "permissions", "lessons:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 70, "permissions", "lessons:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 71, "permissions", "lessons:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 72, "permissions", "lessons:delete", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 73, "permissions", "sections:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 74, "permissions", "sections:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 75, "permissions", "sections:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 76, "permissions", "sections:delete", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 77, "permissions", "reviews:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 78, "permissions", "reviews:add", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 79, "permissions", "reviews:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 80, "permissions", "reviews:delete", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 81, "permissions", "profile:read", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 82, "permissions", "profile:update", "01a095ed-eff4-7d35-917c-b136c6dec33f" },
                    { 83, "permissions", "courses:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 84, "permissions", "courses:add", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 85, "permissions", "courses:update", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 86, "permissions", "courses:delete", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 87, "permissions", "sections:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 88, "permissions", "sections:add", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 89, "permissions", "sections:update", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 90, "permissions", "sections:delete", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 91, "permissions", "lessons:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 92, "permissions", "lessons:add", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 93, "permissions", "lessons:update", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 94, "permissions", "lessons:delete", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 95, "permissions", "categories:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 96, "permissions", "reviews:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 97, "permissions", "profile:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 98, "permissions", "profile:update", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 99, "permissions", "enrollments:read", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 100, "permissions", "enrollments:update", "01a095ed-eff4-7f1e-9b09-951b5cafecde" },
                    { 101, "permissions", "categories:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 102, "permissions", "courses:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 103, "permissions", "enrollments:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 104, "permissions", "enrollments:add", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 105, "permissions", "sections:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 106, "permissions", "lessons:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 107, "permissions", "reviews:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 108, "permissions", "reviews:add", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 109, "permissions", "reviews:update", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 110, "permissions", "reviews:delete", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 111, "permissions", "profile:read", "01a095ed-eff4-708d-94d3-fcd6dda415e4" },
                    { 112, "permissions", "profile:update", "01a095ed-eff4-708d-94d3-fcd6dda415e4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValues: new object[] { 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76,
                    77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101,
                    102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112 });
        }
    }
}
