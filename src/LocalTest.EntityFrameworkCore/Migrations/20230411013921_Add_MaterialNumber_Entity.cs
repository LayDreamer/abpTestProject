using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalTest.Migrations
{
    public partial class Add_MaterialNumber_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialNumber",
                table: "AppMaterialSpecificationDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialNumber",
                table: "AppMaterialSpecificationDetails");
        }
    }
}
