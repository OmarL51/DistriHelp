using Microsoft.EntityFrameworkCore.Migrations;

namespace DistriHelp.API.Migrations
{
    public partial class adingaaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attachments",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
