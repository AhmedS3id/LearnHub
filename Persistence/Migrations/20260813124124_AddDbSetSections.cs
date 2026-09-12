using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHub_Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSetSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Section_SectionId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_AspNetUsers_CreatedById",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_AspNetUsers_UpdatedById",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Courses_CourseId",
                table: "Section");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Section",
                table: "Section");

            migrationBuilder.RenameTable(
                name: "Section",
                newName: "Sections");

            migrationBuilder.RenameIndex(
                name: "IX_Section_UpdatedById",
                table: "Sections",
                newName: "IX_Sections_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Section_CreatedById",
                table: "Sections",
                newName: "IX_Sections_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Section_CourseId",
                table: "Sections",
                newName: "IX_Sections_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sections",
                table: "Sections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Sections_SectionId",
                table: "Lessons",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById",
                table: "Sections",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_UpdatedById",
                table: "Sections",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Sections_SectionId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_UpdatedById",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Courses_CourseId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sections",
                table: "Sections");

            migrationBuilder.RenameTable(
                name: "Sections",
                newName: "Section");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_UpdatedById",
                table: "Section",
                newName: "IX_Section_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CreatedById",
                table: "Section",
                newName: "IX_Section_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Sections_CourseId",
                table: "Section",
                newName: "IX_Section_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Section",
                table: "Section",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Section_SectionId",
                table: "Lessons",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_AspNetUsers_CreatedById",
                table: "Section",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_AspNetUsers_UpdatedById",
                table: "Section",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Courses_CourseId",
                table: "Section",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
