using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Learning_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVocabulary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Emoji",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishExample",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishMeaning",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NativeExample",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NativeMeaning",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Emoji",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "EnglishExample",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "EnglishMeaning",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "NativeExample",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "NativeMeaning",
                table: "Vocabularies");
        }
    }
}
