using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopEdmx.Migrations
{
    public partial class NewMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE GetBySearch
	                    @search nvarchar(max)=null
                    AS
                    BEGIN
	                    select * from [dbo].Product as p
	                    left join [dbo].Category c on p.CategoryId = c.CategoryId
	                    where
	                    p.ProductName Like case when 
	                    @search is not null then '%'+@search+'%' end
	                    or
	                    c.CategoryName Like case when 
	                    @search is not null then '%'+@search+'%' end
                    END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
