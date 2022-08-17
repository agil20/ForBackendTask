using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PypTask.Migrations
{
    public partial class initialProjec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelUploads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(nullable: true),
                    Product = table.Column<int>(nullable: false),
                    DisCountBand = table.Column<int>(nullable: false),
                    UnitsSold = table.Column<int>(nullable: false),
                    ManuFactor = table.Column<int>(nullable: false),
                    SalePrice = table.Column<int>(nullable: false),
                    GrossSales = table.Column<int>(nullable: false),
                    DisCounts = table.Column<int>(nullable: false),
                    Sales = table.Column<int>(nullable: false),
                    Cogs = table.Column<int>(nullable: false),
                    Profit = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelUploads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelUploads");
        }
    }
}
