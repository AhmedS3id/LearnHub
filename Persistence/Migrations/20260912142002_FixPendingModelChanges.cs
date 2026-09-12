using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnHub_Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01a091c7-7e6b-7829-9551-3f0bda1a579f", "01a091c7-7e6b-7370-bae9-f959e448dea7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01a091c7-7e6b-7370-bae9-f959e448dea7");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "RoleId",
                value: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "RoleId",
                value: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                column: "RoleId",
                value: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01a095ed-eff4-708d-94d3-fcd6dda415e4", "01a095ed-eff4-7872-a394-da5724758a8a", true, false, "Member", "MEMBER" },
                    { "01a095ed-eff4-7d35-917c-b136c6dec33f", "01a095ed-eff4-798c-93e9-02ebcf95f00b", false, false, "Admin", "ADMIN" },
                    { "01a095ed-eff4-7f1e-9b09-951b5cafecde", "01a095ed-eff4-76fd-bc6a-6b2661ccb43d", false, false, "Instructor", "INSTRUCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01a095ed-eff4-7fe1-b8a9-e4eafa70f1e0", 0, "01a095ed-eff4-7713-bc82-00f69bad6b75", "admin@LearnHub.com", true, "LearnHub", false, "Admin", false, null, "ADMIN@LEARNHUB.COM", "ADMIN@LEARNHUB.COM", "AQAAAAIAAYagAAAAEBYocXB8KArWfHdt/nkNVt4ZJl+6o09BRIdEnR2WXpJDyiTwpguILscEOmiMTJB96g==", null, false, "FEB01F226B4D496B84DC0CC628C4DD5A", false, "admin@LearnHub.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01a095ed-eff4-7d35-917c-b136c6dec33f", "01a095ed-eff4-7fe1-b8a9-e4eafa70f1e0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a095ed-eff4-708d-94d3-fcd6dda415e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a095ed-eff4-7f1e-9b09-951b5cafecde");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01a095ed-eff4-7d35-917c-b136c6dec33f", "01a095ed-eff4-7fe1-b8a9-e4eafa70f1e0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a095ed-eff4-7d35-917c-b136c6dec33f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01a095ed-eff4-7fe1-b8a9-e4eafa70f1e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26,
                column: "RoleId",
                value: "01a091c7-7e6b-7829-9551-3f0bda1a579f");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44,
                column: "RoleId",
                value: "01a091c7-7e6b-729b-be98-1a6597a86216");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56,
                column: "RoleId",
                value: "01a091c7-7e6b-73d8-96e4-0224784a28f4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01a091c7-7e6b-729b-be98-1a6597a86216", "01a091c7-7e6b-7d43-ad53-cef58e434266", false, false, "Instructor", "INSTRUCTOR" },
                    { "01a091c7-7e6b-73d8-96e4-0224784a28f4", "01a091c7-7e6b-7c20-a72e-2f4a8c1c6242", true, false, "Member", "MEMBER" },
                    { "01a091c7-7e6b-7829-9551-3f0bda1a579f", "01a091c7-7e6b-76d0-abeb-042d14a3dae7", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01a091c7-7e6b-7370-bae9-f959e448dea7", 0, "01a091c7-7e6b-7fdf-8f5a-c2cf774e240c", "admin@LearnHub.com", true, "LearnHub", false, "Admin", false, null, "ADMIN@LEARNHUB.COM", "ADMIN@LEARNHUB.COM", "AQAAAAIAAYagAAAAEBYocXB8KArWfHdt/nkNVt4ZJl+6o09BRIdEnR2WXpJDyiTwpguILscEOmiMTJB96g==", null, false, "FEB01F226B4D496B84DC0CC628C4DD5A", false, "admin@LearnHub.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01a091c7-7e6b-7829-9551-3f0bda1a579f", "01a091c7-7e6b-7370-bae9-f959e448dea7" });
        }
    }
}
