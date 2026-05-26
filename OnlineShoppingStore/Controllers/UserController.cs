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
    public class UserController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();

        private readonly IHostingEnvironment hostingEnvo;

        public UserController(IHostingEnvironment hostingEnvo)
        {

            this.hostingEnvo = hostingEnvo;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(new UserViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<UserDetail>().GetAllRecordsIQueryable().ToList()
            }));
        }
        public ActionResult Create()
        {
            return PartialView(new UserViewModel
            {
                ddlCategory= populator.GetIntStringPairModel("Category"),
                ddlIsActive = populator.GetPairModel("IsActive"),
                dbModel = new UserDetail
                {
                    IsDelete = false,
                    CreatedDate = DateTime.Now
                }

            });
        }
        
        public ActionResult Edit(int id)
        {
            return PartialView(new UserViewModel
            {
                ddlCategory = populator.GetIntStringPairModel("Category"),
                dbModel = _unitOfWork.GetRepositoryInstance<UserDetail>().GetFirstorDefault(id),
                ddlIsActive = populator.GetPairModel("IsActive"),
            });
        }

        
    }
}