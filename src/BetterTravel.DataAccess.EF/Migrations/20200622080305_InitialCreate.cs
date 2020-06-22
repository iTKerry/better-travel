using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Chat",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Subscribed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotTour",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StarsCount = table.Column<int>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    DetailsLink = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<int>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    CountryDetailsLink = table.Column<string>(nullable: true),
                    ResortName = table.Column<string>(nullable: true),
                    ResortDetailsLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotTour", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HotTour",
                schema: "dbo");
        }
    }
}
