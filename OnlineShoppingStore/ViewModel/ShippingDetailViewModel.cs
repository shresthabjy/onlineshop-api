
using OnlineShopEdmx.Model;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class ShippingDetailViewModel
    {
        public ShippingModel dbModel { get; set; }
    }

    public class ShippingDetailViewModelLst
    {
        public IEnumerable<Shippingdetail> dbModelLst { get; set; }
    }
}
