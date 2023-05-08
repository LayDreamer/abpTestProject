using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalTest.Migrations
{
    public partial class Fix_Libs_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppFamilyLibs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "AppFamilyLibs",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "AppFamilyLibs",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "AppFamilyLibs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "AppFamilyLibs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "AppFamilyLibs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UploadUser",
                table: "AppFamilyLibs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "AppFamilyLibs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "AppFamilyLibs",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "UploadUser",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "AppFamilyLibs");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "AppFamilyLibs");
        }
    }
}
