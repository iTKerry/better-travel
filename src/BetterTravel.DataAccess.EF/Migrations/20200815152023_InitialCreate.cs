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
                name: "Currency",
                schema: "dbo",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
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
                    ChatSettingsID = table.Column<int>(nullable: false),
                    IsSubscribed = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSettings", x => x.ChatSettingsID);
                    table.ForeignKey(
                        name: "FK_ChatSettings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatSettings_Chat_ChatSettingsID",
                        column: x => x.ChatSettingsID,
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
                name: "ChatCountrySubscription",
                schema: "dbo",
                columns: table => new
                {
                    ChatCountrySubscriptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatCountrySubscription", x => x.ChatCountrySubscriptionID);
                    table.ForeignKey(
                        name: "FK_ChatCountrySubscription_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatCountrySubscription_ChatSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "ChatSettings",
                        principalColumn: "ChatSettingsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatDepartureSubscription",
                schema: "dbo",
                columns: table => new
                {
                    ChatDepartureSubscriptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsId = table.Column<int>(nullable: true),
                    DepartureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatDepartureSubscription", x => x.ChatDepartureSubscriptionID);
                    table.ForeignKey(
                        name: "FK_ChatDepartureSubscription_DepartureLocation_DepartureId",
                        column: x => x.DepartureId,
                        principalSchema: "dbo",
                        principalTable: "DepartureLocation",
                        principalColumn: "DepartureLocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatDepartureSubscription_ChatSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "ChatSettings",
                        principalColumn: "ChatSettingsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSettings_CurrencyId",
                table: "ChatSettings",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatCountrySubscription_CountryId",
                schema: "dbo",
                table: "ChatCountrySubscription",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatCountrySubscription_SettingsId",
                schema: "dbo",
                table: "ChatCountrySubscription",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDepartureSubscription_DepartureId",
                schema: "dbo",
                table: "ChatDepartureSubscription",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDepartureSubscription_SettingsId",
                schema: "dbo",
                table: "ChatDepartureSubscription",
                column: "SettingsId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatCountrySubscription",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatDepartureSubscription",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HotTour",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatSettings");

            migrationBuilder.DropTable(
                name: "HotelCategory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DepartureLocation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Chat",
                schema: "dbo");
        }
    }
}
