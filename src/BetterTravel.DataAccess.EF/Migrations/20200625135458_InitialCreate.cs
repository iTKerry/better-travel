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
                    ChatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    IsSubscribed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "dbo",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "DepartureLocation",
                schema: "dbo",
                columns: table => new
                {
                    DepartureLocationID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartureLocation", x => x.DepartureLocationID);
                });

            migrationBuilder.CreateTable(
                name: "HotTour",
                schema: "dbo",
                columns: table => new
                {
                    HotTourID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    StarsCount = table.Column<int>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    DetailsLink = table.Column<string>(nullable: true),
                    DepartureLocationId = table.Column<int>(nullable: true),
                    DurationCount = table.Column<int>(nullable: true),
                    DurationType = table.Column<int>(nullable: true),
                    PriceAmount = table.Column<int>(nullable: true),
                    PriceType = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    ResortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotTour", x => x.HotTourID);
                    table.ForeignKey(
                        name: "FK_HotTour_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HotTour_DepartureLocation_DepartureLocationId",
                        column: x => x.DepartureLocationId,
                        principalSchema: "dbo",
                        principalTable: "DepartureLocation",
                        principalColumn: "DepartureLocationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotTour_CountryId",
                schema: "dbo",
                table: "HotTour",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_HotTour_DepartureLocationId",
                schema: "dbo",
                table: "HotTour",
                column: "DepartureLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chat",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HotTour",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DepartureLocation",
                schema: "dbo");
        }
    }
}
