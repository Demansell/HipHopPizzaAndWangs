using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipHopPizzaWangs.Migrations
{
    /// <inheritdoc />
    public partial class ItemsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Items",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Price" },
                values: new object[] { "ferrsef", 12 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "ferrsef", 12 });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "ferrsef", 12 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Items");
        }
    }
}
