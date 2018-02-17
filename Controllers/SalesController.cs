using CorrectTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorrectTemp.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Sales()
        {
            return View();
        }
        public ActionResult totalSales()
        {
            using (var context = new BWAEntities())
            {
                //var message = context.Carts.SqlQuery("SELECT SUM(total) FROM Cart");
                ViewBag.Total = context.Carts.Sum(x=> x.total);
                ViewBag.Total_Units= context.Carts.Sum(x => x.quantity);

                return View();
            }
        }
    }
}