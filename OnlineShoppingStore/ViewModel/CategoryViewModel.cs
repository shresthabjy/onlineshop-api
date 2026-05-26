
using OnlineShopEdmx.Model;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryDetail dbModel { get; set; }
        public IEnumerable<PairModel> ddlIsActive { get; set; }
    }

    public class CategoryViewModelLst
    {
        public IEnumerable<CategoryDetail> dbModelLst { get; set; }
    }
}
