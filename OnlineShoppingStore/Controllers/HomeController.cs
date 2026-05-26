using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopEdmx.Model;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using OnlineShoppingStore.Models;
using OnlineShopEdmx;
using Microsoft.AspNetCore.Http;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if(cart != null) { 
            ViewBag.totalCart = cart.Count();
            }
            else
            {
                ViewBag.totalCart = 0;
            }
            return await Task.Run(() => View(new HomeViewModelLst
            {
                dbModelLst = _unitOfWork.GetRepositoryInstance<ProductDetail>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList()
            }));
        }
        [HttpPost]
        public async Task<IActionResult> SearchResult(string search)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
            {
                ViewBag.totalCart = cart.Count();
            }
            else
            {
                ViewBag.totalCart = 0;
            }
            eCommerceEntities entity = new eCommerceEntities();

            return await Task.Run(() => View(new HomeViewModelLst
            {
                dbModelLst = entity.Product.Where(x => x.ProductName == search && x.IsActive== true ).ToList()
        }));
            

        }


        public ActionResult AddToCart(int id)
        {
            eCommerceEntities entity = new eCommerceEntities();
            var product = entity.Product.Find(id);
            if (SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                cart.Add(new CartViewModel { dbModelLst = product , Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartViewModel { dbModelLst = product, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }


        public IActionResult RemoveCart(int id)
        {
            eCommerceEntities entity = new eCommerceEntities();
            var product = entity.Product.Find(id);
            if (SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity--;
                    if(cart[index].Quantity == 0)
                    {
                        cart.RemoveAt(index);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    }
                }
                else
                {
                    cart.Add(new CartViewModel { dbModelLst = product, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            
            
            return RedirectToAction("Index");
        }


        private int isExist(int id)
        {
            List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].dbModelLst.ProductId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
