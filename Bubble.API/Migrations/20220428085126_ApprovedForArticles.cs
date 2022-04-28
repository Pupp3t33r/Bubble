using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bubble.Data.Migrations
{
    public partial class ApprovedForArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "Articles");
        }
    }
}
