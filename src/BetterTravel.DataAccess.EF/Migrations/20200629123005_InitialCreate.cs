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
                    TelegramChatID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true)
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
                name: "HotelCategory",
                schema: "dbo",
                columns: table => new
                {
                    HotelCategoryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelCategory", x => x.HotelCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ChatSettings",
                columns: table => new
                {
                    ChatSettingsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSubscribed = table.Column<bool>(nullable: false),
                    SettingsOfChatID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSettings", x => x.ChatSettingsID);
                    table.ForeignKey(
                        name: "FK_ChatSettings_Chat_SettingsOfChatID",
                        column: x => x.SettingsOfChatID,
                        principalSchema: "dbo",
                        principalTable: "Chat",
                        principalColumn: "ChatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotTour",
                schema: "dbo",
                columns: table => new
                {
                    HotTourID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: true),
                    ImageLink = table.Column<string>(nullable: true),
                    DetailsLink = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
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
                        name: "FK_HotTour_HotelCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "HotelCategory",
                        principalColumn: "HotelCategoryID",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "SettingsCountry",
                schema: "dbo",
                columns: table => new
                {
                    SettingsCountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsCountry", x => x.SettingsCountryID);
                    table.ForeignKey(
                        name: "FK_SettingsCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SettingsCountry_ChatSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "ChatSettings",
                        principalColumn: "ChatSettingsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSettings_SettingsOfChatID",
                table: "ChatSettings",
                column: "SettingsOfChatID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotTour_CategoryId",
                schema: "dbo",
                table: "HotTour",
                column: "CategoryId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SettingsCountry_CountryId",
                schema: "dbo",
                table: "SettingsCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingsCountry_SettingsId",
                schema: "dbo",
                table: "SettingsCountry",
                column: "SettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotTour",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SettingsCountry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HotelCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DepartureLocation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatSettings");

            migrationBuilder.DropTable(
                name: "Chat",
                schema: "dbo");
        }
    }
}
