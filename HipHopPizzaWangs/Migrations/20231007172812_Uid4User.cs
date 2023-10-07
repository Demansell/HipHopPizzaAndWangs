using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipHopPizzaWangs.Migrations
{
    public partial class Uid4User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "waeabasjvajsvjka",
                column: "Uid",
                value: "3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "waefaw",
                column: "Uid",
                value: "1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "wawfwaeufoaewfhaew",
                column: "Uid",
                value: "2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Users");
        }
    }
}
