using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub_Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId_CourseId",
                table: "Reviews",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_StudentId_CourseId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews",
                column: "StudentId");
        }
    }
}
