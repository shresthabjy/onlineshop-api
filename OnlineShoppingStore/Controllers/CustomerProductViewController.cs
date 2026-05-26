
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{
    public class CustomerProductViewController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();

        private readonly IHostingEnvironment hostingEnvo;

        public CustomerProductViewController(IHostingEnvironment hostingEnvo)
        {

            this.hostingEnvo = hostingEnvo;
        }
        public async Task<IActionResult> Index(int id)
        {
            return await Task.Run(() => View(new CustomerProductSeeViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<ProductDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false && i.ProductId== id).ToList()
            }));
        }
        
    }
}