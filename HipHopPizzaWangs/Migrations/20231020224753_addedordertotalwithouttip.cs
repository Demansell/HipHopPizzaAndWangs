﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipHopPizzaWangs.Migrations
{
    /// <inheritdoc />
    public partial class addedordertotalwithouttip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderTotalWithoutTip",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderTotalWithoutTip",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderTotalWithoutTip",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderTotalWithoutTip",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotalWithoutTip",
                table: "Orders");
        }
    }
}
