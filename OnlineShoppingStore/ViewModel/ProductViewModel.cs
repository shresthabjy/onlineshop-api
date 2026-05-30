
using Microsoft.AspNetCore.Http;
using OnlineShopEdmx.Model;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class ProductViewModel
    {
        public ProductDetail dbModel { get; set; }
        public IEnumerable<PairModel> ddlIsActive { get; set; }
        public IEnumerable<IntStringPairModel> ddlCategory { get; set; }
        public IEnumerable<IntStringPairModel> ddlProductFeature { get; set; }
        
        public IFormFile productImage { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int ProductFeatureId { get; set; }

    }
    public class ProductViewModelApi
    {
        public IFormFile image { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int ProductFeatureId { get; set; }

    }
    public class ProductViewModelLst
    {
        public IEnumerable<ProductDetail> dbModelLst { get; set; }
    }
}
