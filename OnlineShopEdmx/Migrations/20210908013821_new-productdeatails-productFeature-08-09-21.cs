using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopEdmx.Migrations
{
    public partial class newproductdeatailsproductFeature080921 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "ProductFeatureId",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductFeatureId",
                table: "Product");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Product",
                type: "bit",
                nullable: true);
        }
    }
}
