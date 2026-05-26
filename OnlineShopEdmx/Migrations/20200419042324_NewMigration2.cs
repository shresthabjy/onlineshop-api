using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopEdmx.Migrations
{
    public partial class NewMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<DateTime>(nullable: false),
                    ProductImage = table.Column<string>(nullable: true),
                    IsFeatured = table.Column<bool>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
