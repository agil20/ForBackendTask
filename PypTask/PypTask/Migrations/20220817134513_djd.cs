using Microsoft.EntityFrameworkCore.Migrations;

namespace PypTask.Migrations
{
    public partial class djd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Sales",
                table: "ExcelUploads",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "SalePrice",
                table: "ExcelUploads",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Profit",
                table: "ExcelUploads",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sales",
                table: "ExcelUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "SalePrice",
                table: "ExcelUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Profit",
                table: "ExcelUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
