using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;

namespace OnlineShopping.Controllers
{
    public class ProductFeatureController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(new ProductFeatureViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
        }));
        }
        public ActionResult Create()
        {
            return PartialView(new ProductFeatureViewModel
            {
                ddlIsActive = populator.GetPairModel("IsActive"),
                dbModel = new ProductFeatureDetail { 
                    IsActive = true,
                    IsDelete = false
                }

            });
        }
        [HttpPost]
        public ActionResult Create(ProductFeatureViewModel tbl)
        {
           
            _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().Add(tbl.dbModel);
            return RedirectToAction("Create");
        }
        public ActionResult Edit(int id)
        {
            return PartialView(new ProductFeatureViewModel
            {
                dbModel = _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().GetFirstorDefault(id),
                ddlIsActive = populator.GetPairModel("IsActive"),
            });
        }
        [HttpPost]
        public ActionResult Edit(ProductFeatureViewModel tbl)
        {
            _unitOfWork.GetRepositoryInstance<ProductFeatureDetail>().Update(tbl.dbModel);
            return RedirectToAction("Index");
        }

    }
}