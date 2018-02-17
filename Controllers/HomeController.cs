using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BWA_CB006302.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
        public ActionResult admin_Index()
        {
            ViewBag.Title = "admin_Index";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View("Contact");
        }
        public ActionResult myAccount()
        {
            ViewBag.Title = "My Account";
            return View("myAccount");
        }
        public ActionResult Cart()
        {
            ViewBag.Title = "Cart";
            return View("Cart");
        }
        public ActionResult Checkout()
        {
            ViewBag.Title = "Checkout";
            return View("Checkout");
        }
    }
}