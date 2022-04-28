using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bubble.Data.Migrations
{
    public partial class GoodnessRatingRises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodnessIndex",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "GoodnessRating",
                table: "Articles",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodnessRating",
                table: "Articles");

            migrationBuilder.AddColumn<decimal>(
                name: "GoodnessIndex",
                table: "Articles",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
