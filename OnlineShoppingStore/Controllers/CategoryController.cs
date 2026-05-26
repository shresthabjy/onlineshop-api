using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShopEdmx.Model;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;

namespace OnlineShopping.Controllers
{
    public class CategoryController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(new CategoryViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<CategoryDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
        }));
        }
        public ActionResult Create()
        {
            return PartialView(new CategoryViewModel
            {
                ddlIsActive = populator.GetPairModel("IsActive"),
                dbModel = new CategoryDetail { 
                    IsActive = true,
                    IsDelete = false
                }

            });
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel tbl)
        {
           
            _unitOfWork.GetRepositoryInstance<CategoryDetail>().Add(tbl.dbModel);
            return RedirectToAction("Create");
        }
        public ActionResult Edit(int id)
        {
            return PartialView(new CategoryViewModel
            {
                dbModel = _unitOfWork.GetRepositoryInstance<CategoryDetail>().GetFirstorDefault(id),
                ddlIsActive = populator.GetPairModel("IsActive"),
            });
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel tbl)
        {
            _unitOfWork.GetRepositoryInstance<CategoryDetail>().Update(tbl.dbModel);
            return RedirectToAction("Index");
        }

 

    }
}