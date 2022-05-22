using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations
{
    public partial class FixNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Division_DivisionId",
                table: "Workers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "Worker");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Division",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DateOfFormation",
                table: "Division",
                newName: "FormationDate");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Worker",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Worker",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "DrivingLicense",
                table: "Worker",
                newName: "IsHasDriveLicense");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Worker",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_DivisionId",
                table: "Worker",
                newName: "IX_Worker_DivisionId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Division",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Worker",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Worker",
                table: "Worker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Worker_Division_DivisionId",
                table: "Worker",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worker_Division_DivisionId",
                table: "Worker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Worker",
                table: "Worker");

            migrationBuilder.RenameTable(
                name: "Worker",
                newName: "Workers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Division",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "FormationDate",
                table: "Division",
                newName: "DateOfFormation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "IsHasDriveLicense",
                table: "Workers",
                newName: "DrivingLicense");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Workers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Workers",
                newName: "DateOfBirth");

            migrationBuilder.RenameIndex(
                name: "IX_Worker_DivisionId",
                table: "Workers",
                newName: "IX_Workers_DivisionId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Division",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Division_DivisionId",
                table: "Workers",
                column: "DivisionId",
                principalTable: "Division",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
