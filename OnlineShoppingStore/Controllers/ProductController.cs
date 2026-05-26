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
    public class ProductController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Populator populator = new Populator();

        private readonly IWebHostEnvironment hostingEnvo;

        public ProductController(IWebHostEnvironment hostingEnvo)
        {

            this.hostingEnvo = hostingEnvo;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(new ProductViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<ProductDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            }));
        }
        public ActionResult Create()
        {
            return PartialView(new ProductViewModel
            {
                ddlCategory= populator.GetIntStringPairModel("Category"),
                ddlProductFeature= populator.GetIntStringPairModel("ProductFeature"),
                ddlIsActive = populator.GetPairModel("IsActive"),
                dbModel = new ProductDetail
                {
                    IsDelete = false,
                    CreatedDate = DateTime.Now
                }

            });
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel tbl, IFormFile file)
        {
            string uniqueFileName = null;
            if (tbl.image != null)
            {
                if (imageValidation(tbl.image.FileName) == true)
                {
                    string uploadsFolder = Path.Combine(hostingEnvo.WebRootPath, "productImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + tbl.image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    tbl.image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            tbl.dbModel.ProductImage = uniqueFileName;
            tbl.dbModel.CreatedDate = tbl.dbModel.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<ProductDetail>().Add(tbl.dbModel);
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            return PartialView(new ProductViewModel
            {
                dbModel = _unitOfWork.GetRepositoryInstance<ProductDetail>().GetFirstorDefault(id),
                ddlIsActive = populator.GetPairModel("IsActive"),
                ddlCategory = populator.GetIntStringPairModel("Category"),
                ddlProductFeature = populator.GetIntStringPairModel("ProductFeature")
            });
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel tbl)
        {
            string uniqueFileName = null;
            if (tbl.image != null)
            {
                if (imageValidation(tbl.image.FileName) == true)
                {
                    string uploadsFolder = Path.Combine(hostingEnvo.WebRootPath, "productImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + tbl.image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    tbl.image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            tbl.dbModel.ProductImage = uniqueFileName;
            tbl.dbModel.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<ProductDetail>().Update(tbl.dbModel);
            return RedirectToAction("Index");
        }
        public bool imageValidation(String imageName = "null.doc")
        {
            string ext = System.IO.Path.GetExtension(imageName);
            switch (ext)
            {
                case (".jpg"):
                    return true;
                    break;
                case ".png":
                    return true;
                    break;
                case ".gif":
                    return true;
                    break;
                case ".jpeg":
                    return true;
                    break;
                case null:
                    return true;
                    break;
                default:
                    return false;
            }
        }
    }
}