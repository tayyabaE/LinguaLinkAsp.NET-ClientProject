using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Learning_System.Migrations
{
    /// <inheritdoc />
    public partial class VocabularyLinkedToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vocabularies_Courses_CourseId",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "Example",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "Meaning",
                table: "Vocabularies");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Vocabularies",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Vocabularies_CourseId",
                table: "Vocabularies",
                newName: "IX_Vocabularies_LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabularies_Lessons_LessonId",
                table: "Vocabularies",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vocabularies_Lessons_LessonId",
                table: "Vocabularies");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Vocabularies",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Vocabularies_LessonId",
                table: "Vocabularies",
                newName: "IX_Vocabularies_CourseId");

            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Meaning",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabularies_Courses_CourseId",
                table: "Vocabularies",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
