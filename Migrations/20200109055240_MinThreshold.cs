using Microsoft.EntityFrameworkCore.Migrations;

namespace AtlantisPortals.API.Migrations
{
    public partial class MinThreshold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinThreshold",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "MinUnitThreshold",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinUnitThreshold",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "MinThreshold",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
