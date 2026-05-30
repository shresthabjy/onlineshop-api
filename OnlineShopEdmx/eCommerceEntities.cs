using Amazon.OpsWorks.Model;
using Microsoft.EntityFrameworkCore;
using OnlineShopEdmx.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShopEdmx
{
    public class eCommerceEntities : DbContext
    {
       

        public DbSet<ProductFeatureDetail> ProductFeature { get; set; }
        public DbSet<CategoryDetail> Category{ get; set; }
        public DbSet<ProductDetail> Product{ get; set; }
        public DbSet<UserDetail> User { get; set; }
        public DbSet<ShippingDetail> Shipping { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //for azure
            optionsBuilder.UseSqlServer(@"Server=tcp:onlineshopserverbijay.database.windows.net,1433;Initial Catalog=free-sql-db-7650359;Persist Security Info=False;User ID=bijayadmin;Password=bStha123!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            
            //forlocal connection
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-PUQ031I;Database=OnlineShopping;Trusted_Connection=True;MultipleActiveResultSets=true");
            //app.UseAuthentication();
        }

    }
}
