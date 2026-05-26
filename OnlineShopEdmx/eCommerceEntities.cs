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
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PUQ031I;Database=OnlineShopping;Trusted_Connection=True;MultipleActiveResultSets=true");
            //app.UseAuthentication();
        }

    }
}
