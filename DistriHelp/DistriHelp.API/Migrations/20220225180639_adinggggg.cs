using Microsoft.EntityFrameworkCore.Migrations;

namespace DistriHelp.API.Migrations
{
    public partial class adinggggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attachments",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "Requests");
        }
    }
}
