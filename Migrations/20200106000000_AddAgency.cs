using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtlantisPortals.API.Migrations
{
    public partial class AddAgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Ministry = table.Column<string>(maxLength: 200, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 150, nullable: true),
                    ContactNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Prefix = table.Column<string>(maxLength: 10, nullable: false),
                    DigitLength = table.Column<int>(nullable: false),
                    ReceiptType = table.Column<string>(maxLength: 100, nullable: false),
                    UnitPerReceiptType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
