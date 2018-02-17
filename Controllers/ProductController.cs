using CorrectTemp.Controllers;
using CorrectTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BWA_CB006302.Controllers
{
    public class ProductController : Controller
    {
        BWAEntities DbProd = new BWAEntities();
        BWAEntities DbCart = new BWAEntities();

        public ActionResult Product()
        {
            using (var context = new BWAEntities())
            {
                var men_product = context.Products.SqlQuery("select * from Product where ItemCategory='Men'").ToList();

                return View(men_product);
            }
        }
        public ActionResult women()
        {
            using (var context = new BWAEntities())
            {
                var women_product = context.Products.SqlQuery("select * from Product where ItemCategory='Women'").ToList();

                return View(women_product);
            }
        }
        public ActionResult add_to_cart(string id)
        {
            BWAEntities context = new BWAEntities();
            if (Session["cart"] == null)
            {
                List<Product> cart = new List<Product>();
                cart.Add(context.Products.Single(x=> x.ItemID == id));
                Session["cart"] = cart;
            }
            else
            {
                List<Product> cart = (List<Product>)Session["cart"];
                int a = addExistingItem(id);
                if (a == -1)
                    cart.Add(context.Products.Single(x => x.ItemID == id));
                else
                    cart[a].Quantity++;
                    Session["cart"] = cart;
            }
            return View("Cart");
        }
        public ActionResult delete(string id) {

            int a = addExistingItem(id);
            List<Product> cart = (List<Product>)Session["cart"];
            cart.RemoveAt(a);
            Session["cart"] = cart;
            return View("Cart");
        }
        public ActionResult ProductDetails()
        {
            ViewBag.Title = "ProductDetails";
            return View();
        }
        private int addExistingItem(string id)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ItemID == id)
                    return i;
            return -1;
        }
        public ActionResult checkout_cart()
        {
            List<Product> cart_ = (List<Product>)Session["cart"];

            if (CorrectTemp.DataAccess.DA.userCart(cart_, (string)Session["userSession"]))
                {
                    ViewBag.Success = "Your purchse was successfully placed.";
                   Session.Remove("cart");
                    return View("../Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "There was a problem, try again!");
                }
            
            return View("Cart");
        }
    }
}