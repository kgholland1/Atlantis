using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtlantisPortals.API.Migrations
{
    public partial class FixPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Agencies_AgentId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_Payment_PaymentId",
                table: "PaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentDetail",
                newName: "PaymentDetails");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetail_PaymentId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_AgentId",
                table: "Payments",
                newName: "IX_Payments_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptNumber = table.Column<string>(maxLength: 30, nullable: false),
                    ParentReceiptNumber = table.Column<string>(maxLength: 30, nullable: true),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    RecipentName = table.Column<string>(maxLength: 100, nullable: true),
                    RecipentEmail = table.Column<string>(maxLength: 100, nullable: true),
                    RecipentPhone = table.Column<string>(maxLength: 25, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false),
                    ReceiptType = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiptUrl = table.Column<string>(maxLength: 150, nullable: true),
                    SecurityCode = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Agencies_AgentId",
                table: "Payments",
                column: "AgentId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Agencies_AgentId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentDetails",
                newName: "PaymentDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_AgentId",
                table: "Payment",
                newName: "IX_Payment_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_PaymentId",
                table: "PaymentDetail",
                newName: "IX_PaymentDetail_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Agencies_AgentId",
                table: "Payment",
                column: "AgentId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_Payment_PaymentId",
                table: "PaymentDetail",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
