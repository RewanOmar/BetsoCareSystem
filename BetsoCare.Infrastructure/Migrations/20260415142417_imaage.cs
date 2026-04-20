using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetsoCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class imaage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Shelters",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Shelters");
        }
    }
}
