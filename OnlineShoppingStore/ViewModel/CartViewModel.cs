
using OnlineShopEdmx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class CartViewModel
    {
        public ProductDetail dbModelLst  { get; set; }
        public int Quantity { get; set; }
    }
}
