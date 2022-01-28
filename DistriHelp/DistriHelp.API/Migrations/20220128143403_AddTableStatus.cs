using Microsoft.EntityFrameworkCore.Migrations;

namespace DistriHelp.API.Migrations
{
    public partial class AddTableStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_Description",
                table: "Solutions",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_Tittle",
                table: "Solutions",
                column: "Tittle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Description",
                table: "Statuses",
                column: "Description",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_Description",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_Tittle",
                table: "Solutions");
        }
    }
}
