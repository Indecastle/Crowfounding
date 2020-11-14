using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Crowfounding.Migrations
{
    public partial class InitTableTotalDonates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [TotalDonate]
                GO

                INSERT INTO [TotalDonate] ([UserId], [CompanyId], [Amount])
                SELECT [UserId], [CompanyId], sum([MonyDonate]) [SumValue]
	                FROM [Donations]
	                GROUP BY [UserId], [CompanyId]
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
