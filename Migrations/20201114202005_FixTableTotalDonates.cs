using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowfounding.Migrations
{
    public partial class FixTableTotalDonates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalDonate_Companies_CompanyId",
                table: "TotalDonate");

            migrationBuilder.DropForeignKey(
                name: "FK_TotalDonate_AspNetUsers_UserId",
                table: "TotalDonate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalDonate",
                table: "TotalDonate");

            migrationBuilder.RenameTable(
                name: "TotalDonate",
                newName: "TotalDonates");

            migrationBuilder.RenameIndex(
                name: "IX_TotalDonate_UserId",
                table: "TotalDonates",
                newName: "IX_TotalDonates_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TotalDonate_CompanyId",
                table: "TotalDonates",
                newName: "IX_TotalDonates_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalDonates",
                table: "TotalDonates",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Bonuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NeedAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bonuses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonuses_CompanyId",
                table: "Bonuses",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TotalDonates_Companies_CompanyId",
                table: "TotalDonates",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TotalDonates_AspNetUsers_UserId",
                table: "TotalDonates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalDonates_Companies_CompanyId",
                table: "TotalDonates");

            migrationBuilder.DropForeignKey(
                name: "FK_TotalDonates_AspNetUsers_UserId",
                table: "TotalDonates");

            migrationBuilder.DropTable(
                name: "Bonuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalDonates",
                table: "TotalDonates");

            migrationBuilder.RenameTable(
                name: "TotalDonates",
                newName: "TotalDonate");

            migrationBuilder.RenameIndex(
                name: "IX_TotalDonates_UserId",
                table: "TotalDonate",
                newName: "IX_TotalDonate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TotalDonates_CompanyId",
                table: "TotalDonate",
                newName: "IX_TotalDonate_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalDonate",
                table: "TotalDonate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TotalDonate_Companies_CompanyId",
                table: "TotalDonate",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TotalDonate_AspNetUsers_UserId",
                table: "TotalDonate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
