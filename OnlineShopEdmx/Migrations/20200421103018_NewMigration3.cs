using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopEdmx.Migrations
{
    public partial class NewMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Description",
                table: "Product",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
