using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterTravel.DataAccess.EF.Migrations
{
    public partial class AddChatId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                schema: "dbo",
                table: "Chat",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                schema: "dbo",
                table: "Chat");
        }
    }
}
