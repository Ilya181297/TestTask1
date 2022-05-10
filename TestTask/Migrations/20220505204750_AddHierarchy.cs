using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    public partial class AddHierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Division",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Division_ParentId",
                table: "Division",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Division_Division_ParentId",
                table: "Division",
                column: "ParentId",
                principalTable: "Division",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Division_Division_ParentId",
                table: "Division");

            migrationBuilder.DropIndex(
                name: "IX_Division_ParentId",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Division");
        }
    }
}
