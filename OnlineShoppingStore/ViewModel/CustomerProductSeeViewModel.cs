
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
    public class CustomerProductSeeViewModel
    {
        public ProductDetail dbModel { get; set; }
        public IEnumerable<PairModel> ddlIsActive { get; set; }
        public IEnumerable<IntStringPairModel> ddlCategory { get; set; }
        public IEnumerable<IntStringPairModel> ddlProductFeature { get; set; }
        public IFormFile image { get; set; }
    }

    public class CustomerProductSeeViewModelLst
    {
        public IEnumerable<ProductDetail> dbModelLst { get; set; }
    }
}
