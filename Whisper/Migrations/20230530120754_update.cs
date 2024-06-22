using Microsoft.EntityFrameworkCore.Migrations;

namespace Whisper.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Communities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Communities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Communities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
