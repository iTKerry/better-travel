using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class DepartureSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsCountry",
                schema: "dbo");

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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatCountrySubscription",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatDepartureSubscription",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "SettingsCountry",
                schema: "dbo",
                columns: table => new
                {
                    SettingsCountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    SettingsId = table.Column<int>(type: "int", nullable: true)
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
    }
}
