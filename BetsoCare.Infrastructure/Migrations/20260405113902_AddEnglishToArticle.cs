using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetsoCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Articles");
        }
    }
}
