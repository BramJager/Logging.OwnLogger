using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore.WeekOpdracht.Business.Migrations
{
    public partial class AddLevelToLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Logs");
        }
    }
}
