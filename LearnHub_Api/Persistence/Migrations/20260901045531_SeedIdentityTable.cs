using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnHub_Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d", "b9a55bef-eb94-433f-81f3-4a25d40a66bb", true, false, "Member", "MEMBER" },
                    { "dd58aa72-8865-430a-8f72-fbf47431c20e", "394f4e90-7bb7-4925-99c4-0d9057f00cce", false, false, "Instructor", "INSTRUCTOR" },
                    { "e7f96ddf-1c51-47e2-b801-90752377a215", "cd72626d-3ed3-4f4a-957e-8921508d23e2", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "60611967-12d7-44c4-b4ba-b6283fc6faca", 0, "15fb381d-86a6-45fb-8b74-8bc152a1f2a7", "admin@LearnHub.com", true, "LearnHub", "Admin", false, null, "ADMIN@LEARNHUB.COM", "ADMIN@LEARNHUB.COM", "AQAAAAIAAYagAAAAEBYocXB8KArWfHdt/nkNVt4ZJl+6o09BRIdEnR2WXpJDyiTwpguILscEOmiMTJB96g==", null, false, "FEB01F226B4D496B84DC0CC628C4DD5A", false, "admin@LearnHub.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "users:manage", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 2, "permissions", "categories:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 3, "permissions", "categories:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 4, "permissions", "categories:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 5, "permissions", "categories:delete", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 6, "permissions", "courses:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 7, "permissions", "courses:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 8, "permissions", "courses:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 9, "permissions", "courses:delete", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 10, "permissions", "enrollments:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 11, "permissions", "enrollments:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 12, "permissions", "enrollments:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 13, "permissions", "lessons:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 14, "permissions", "lessons:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 15, "permissions", "lessons:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 16, "permissions", "lessons:delete", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 17, "permissions", "sections:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 18, "permissions", "sections:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 19, "permissions", "sections:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 20, "permissions", "sections:delete", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 21, "permissions", "reviews:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 22, "permissions", "reviews:add", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 23, "permissions", "reviews:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 24, "permissions", "reviews:delete", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 25, "permissions", "profile:read", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 26, "permissions", "profile:update", "e7f96ddf-1c51-47e2-b801-90752377a215" },
                    { 27, "permissions", "courses:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 28, "permissions", "courses:add", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 29, "permissions", "courses:update", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 30, "permissions", "courses:delete", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 31, "permissions", "sections:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 32, "permissions", "sections:add", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 33, "permissions", "sections:update", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 34, "permissions", "sections:delete", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 35, "permissions", "lessons:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 36, "permissions", "lessons:add", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 37, "permissions", "lessons:update", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 38, "permissions", "lessons:delete", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 39, "permissions", "categories:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 40, "permissions", "reviews:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 41, "permissions", "profile:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 42, "permissions", "profile:update", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 43, "permissions", "enrollments:read", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 44, "permissions", "enrollments:update", "dd58aa72-8865-430a-8f72-fbf47431c20e" },
                    { 45, "permissions", "categories:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 46, "permissions", "courses:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 47, "permissions", "enrollments:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 48, "permissions", "enrollments:add", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 49, "permissions", "sections:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 50, "permissions", "lessons:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 51, "permissions", "reviews:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 52, "permissions", "reviews:add", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 53, "permissions", "reviews:update", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 54, "permissions", "reviews:delete", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 55, "permissions", "profile:read", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" },
                    { 56, "permissions", "profile:update", "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e7f96ddf-1c51-47e2-b801-90752377a215", "60611967-12d7-44c4-b4ba-b6283fc6faca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e7f96ddf-1c51-47e2-b801-90752377a215", "60611967-12d7-44c4-b4ba-b6283fc6faca" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05b8bff1-d50d-4e9e-8dee-2ad9f5bb1b8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd58aa72-8865-430a-8f72-fbf47431c20e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7f96ddf-1c51-47e2-b801-90752377a215");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60611967-12d7-44c4-b4ba-b6283fc6faca");
        }
    }
}
