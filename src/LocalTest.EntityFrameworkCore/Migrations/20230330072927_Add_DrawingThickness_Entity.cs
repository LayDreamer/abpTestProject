using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalTest.Migrations
{
    public partial class Add_DrawingThickness_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawingHeight",
                table: "AppMaterialSpecificationDetails",
                newName: "DrawingThickness");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawingThickness",
                table: "AppMaterialSpecificationDetails",
                newName: "DrawingHeight");
        }
    }
}
