using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowfounding.Migrations
{
    public partial class AddTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Companies_CompanyId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_UserId",
                table: "Transactions",
                newName: "IX_Transactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CompanyId",
                table: "Transactions",
                newName: "IX_Transactions_CompanyId");

            migrationBuilder.AlterColumn<decimal>(
                name: "MonyDonate",
                table: "Donations",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Transactions",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_UserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId",
                table: "Transaction",
                newName: "IX_Transaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transaction",
                newName: "IX_Transaction_CompanyId");

            migrationBuilder.AlterColumn<int>(
                name: "MonyDonate",
                table: "Donations",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Companies_CompanyId",
                table: "Transaction",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
