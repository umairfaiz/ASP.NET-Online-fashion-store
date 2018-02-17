using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorrectTemp.Models;

namespace BWA_CB006302.Controllers
{
    public class CartController : Controller
    {

        BWAEntities DbCart = new BWAEntities();
        public ActionResult Cart()
        {
            return View();
        }

        //public ActionResult checkout_cart()
        //{
        //    List<Product> cart_ = (List<Product>)Session["cart"];
        //    foreach (Product item in cart_)
        //    {
        //        if (CorrectTemp.DataAccess.DA.userCart(item.ItemID, item.Quantity, (int)item.Price, (int)item.Price * item.Quantity, (string)Session["userSession"], item.ItemName))
        //        {
        //            return RedirectToAction("../Home/Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "There was a problem, try again!");
        //        }
        //    }
        //    return View("Cart");
        //}



        //public ActionResult add_to_cart(string id)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        List<Items> cart = new List<Items>();
        //        cart.Add(new Items(DbCart.Products.Find(id), 1));
        //        Session["cart"] = cart;

        //    }
        //    else
        //    {
        //        List<Items> cart = (List<Items>)Session["cart"];
        //        int a = addExistingItem(id);
        //        if (a == -1)
        //            cart.Add(new Items(DbCart.Products.Find(id), 1));
        //        else
        //            cart[a].Quantity++;
        //        Session["cart"] = cart;
        //    }
        //    return View("Cart");
        //}
        //private int addExistingItem(string id)
        //{
        //    List<Items> cart = (List<Items>)Session["cart"];
        //    for (int i = 0; i < cart.Count; i++)
        //        if (cart[i].Product.ItemID == id)
        //            return i;
        //    return -1;
        //}



    }
}