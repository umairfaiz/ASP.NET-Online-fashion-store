using CorrectTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorrectTemp.Controllers
{
    public class WishlistController : Controller
    {

        BWAEntities Dbwlist = new BWAEntities();
        public ActionResult Wishlist()
        {
            return View();
        }
        public ActionResult add_to_wishlist(string id)
        {
            BWAEntities context = new BWAEntities();
            if (Session["wlist"] == null)
            {
                List<Product> wlist = new List<Product>();
                wlist.Add(context.Products.Single(x => x.ItemID == id));
                Session["wlist"] = wlist;

            }
            else
            {
                List<Product> wlist = (List<Product>)Session["wlist"];
                int a = addExisting_wList_Item(id);
                if (a == -1)
                    wlist.Add(context.Products.Single(x => x.ItemID == id));
                else
                    wlist[a].Quantity++;
                Session["wlist"] = wlist;
            }
            return View("Wishlist");
        }
        private int addExisting_wList_Item(string id)
        {
            List<Product> wlist = (List<Product>)Session["wlist"];
            for (int i = 0; i < wlist.Count; i++)
                if (wlist[i].ItemID == id)
                    return i;
            return -1;
        }
        public ActionResult remove_wList_Item(string id)
        {

            int a = addExisting_wList_Item(id);
            List<Product> cart = (List<Product>)Session["wlist"];
            cart.RemoveAt(a);
            Session["wlist"] = cart;
            return View("Wishlist");
        }

        public ActionResult add_WlistDB()
        {

            List<Product> cart = (List<Product>)Session["wlist"];
            if (CorrectTemp.DataAccess.DA.AddtoWlist(cart, (string)Session["userSession"]))
            {
                return RedirectToAction("../Product/Product");
            }
            else
            {
                ViewBag.Message = "There was a problem, Try again!";
                return View("../Product/Product");
            }
        }
        public ActionResult view_Wlist()
        {
            try
            {
                var user = Session["userSession"];
                using (var context = new BWAEntities())
                {
                    var wishlist_view = context.Products.SqlQuery("select * from Wlist where username='" + user + "'");

                    return View("Wishlist", wishlist_view);
                }
            }
            catch(InvalidOperationException oe)
            {
                ViewBag.wlistError = oe;
                return View("../Product/Product");
            }
        }
    }

}