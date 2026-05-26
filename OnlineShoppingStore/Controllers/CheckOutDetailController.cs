using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;

namespace OnlineShopping.Controllers
{
    public class CheckOutDetailController : Controller
    {
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.dbModelLst.Price * item.Quantity);
            if (cart != null)
            {
                ViewBag.totalCart = cart.Count();
            }
            else
            {
                ViewBag.totalCart = 0;
            }
            return View();
        }
    }
}