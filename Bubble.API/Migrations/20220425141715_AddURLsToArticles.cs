using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bubble.Data.Migrations
{
    public partial class AddURLsToArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SourceURL",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "SourceURL",
                table: "Articles");
        }
    }
}
