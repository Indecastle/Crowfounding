using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowfounding.Migrations
{
    public partial class addToTableUserBonuseColumnTotalDonateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [UserBonuses]", true);
            
            migrationBuilder.AddColumn<Guid>(
                name: "TotalDonateId",
                table: "UserBonuses",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserBonuses_TotalDonateId",
                table: "UserBonuses",
                column: "TotalDonateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBonuses_TotalDonates_TotalDonateId",
                table: "UserBonuses",
                column: "TotalDonateId",
                principalTable: "TotalDonates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBonuses_TotalDonates_TotalDonateId",
                table: "UserBonuses");

            migrationBuilder.DropIndex(
                name: "IX_UserBonuses_TotalDonateId",
                table: "UserBonuses");

            migrationBuilder.DropColumn(
                name: "TotalDonateId",
                table: "UserBonuses");
        }
    }
}
