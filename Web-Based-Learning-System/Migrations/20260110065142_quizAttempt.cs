using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Learning_System.Migrations
{
    /// <inheritdoc />
    public partial class quizAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "QuizAttempts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "QuizAttempts");
        }
    }
}
