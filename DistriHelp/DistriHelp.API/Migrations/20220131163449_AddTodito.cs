using Microsoft.EntityFrameworkCore.Migrations;

namespace DistriHelp.API.Migrations
{
    public partial class AddTodito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequesType",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequesTypeId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Id",
                table: "Requests",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesTypeId",
                table: "Requests",
                column: "RequesTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestTypes_RequesTypeId",
                table: "Requests",
                column: "RequesTypeId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestTypes_RequesTypeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_Id",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequesTypeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequesTypeId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Requests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequesType",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
