using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class ChatSettingsCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatCountrySubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatCountrySubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatDepartureSubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatDepartureSubscription");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatCountrySubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatCountrySubscription",
                column: "SettingsId",
                principalTable: "ChatSettings",
                principalColumn: "ChatSettingsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatDepartureSubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatDepartureSubscription",
                column: "SettingsId",
                principalTable: "ChatSettings",
                principalColumn: "ChatSettingsID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatCountrySubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatCountrySubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatDepartureSubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatDepartureSubscription");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatCountrySubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatCountrySubscription",
                column: "SettingsId",
                principalTable: "ChatSettings",
                principalColumn: "ChatSettingsID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatDepartureSubscription_ChatSettings_SettingsId",
                schema: "dbo",
                table: "ChatDepartureSubscription",
                column: "SettingsId",
                principalTable: "ChatSettings",
                principalColumn: "ChatSettingsID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
