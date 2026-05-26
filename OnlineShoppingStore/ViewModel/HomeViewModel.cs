using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class HomeViewModel
    {

        public ProductDetail dbModel { get; set; }
        public IEnumerable<PairModel> ddlIsActive { get; set; }

    }
    public class HomeViewModelLst
    {
        public IEnumerable<ProductDetail> dbModelLst { get; set; }
    }
}
