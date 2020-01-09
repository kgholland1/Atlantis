using Microsoft.EntityFrameworkCore.Migrations;

namespace AtlantisPortals.API.Migrations
{
    public partial class AddReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Agencies_AgentId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AgentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AgencyId",
                table: "Payments",
                column: "AgencyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Agencies_AgencyId",
                table: "Payments",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Agencies_AgencyId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_AgencyId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AgentId",
                table: "Payments",
                column: "AgentId",
                unique: true,
                filter: "[AgentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Agencies_AgentId",
                table: "Payments",
                column: "AgentId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
