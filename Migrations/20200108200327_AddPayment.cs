using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtlantisPortals.API.Migrations
{
    public partial class AddPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptType",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "UnitPerReceiptType",
                table: "Agencies");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ReceiptTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountBalance = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    ReceiptType = table.Column<string>(maxLength: 100, nullable: false),
                    UnitPerReceiptType = table.Column<int>(nullable: false),
                    CurrentReceiptNumber = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: true),
                    AgencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Agencies_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(maxLength: 60, nullable: false),
                    PaymentGatewayRef = table.Column<string>(maxLength: 150, nullable: true),
                    PaymentVerified = table.Column<bool>(nullable: false),
                    CAGDAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    PDQAmount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    AgencyName = table.Column<string>(maxLength: 200, nullable: true),
                    PaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AgentId",
                table: "Payment",
                column: "AgentId",
                unique: true,
                filter: "[AgentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_PaymentId",
                table: "PaymentDetail",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ReceiptTypes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptType",
                table: "Agencies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UnitPerReceiptType",
                table: "Agencies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
