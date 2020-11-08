using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowfounding.Migrations
{
    public partial class FixTypeOfMoneyForCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NeedMoney",
                table: "Companies",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentMoney",
                table: "Companies",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NeedMoney",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentMoney",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");
        }
    }
}
