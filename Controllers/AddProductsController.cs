using BWA_CB006302.Models;
using CorrectTemp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BWA_CB006302.Controllers
{
    public class AddProductsController : Controller
    {
        // GET: AddProducts
        public ActionResult AddProducts()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AddProducts(upload_image upload)
        {
            Product pro = new Product();
            if(upload.file.ContentLength > 0){
                var fileName = Path.GetFileName(upload.file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/img"), fileName);
                upload.file.SaveAs(path);
            }
            return View("addProductDetails");
        }

        public ActionResult addProductDetails()
        {
            Product pro = new Product();
            pro.ItemID = Request["itemID"];
            pro.ItemName= Request["ItemName"];
            pro.ItemDescription = Request["ItemDescription"];
            pro.Stock=  Convert.ToInt32(Request["Stock"]);
            pro.Discount = Convert.ToInt32(Request["Discount"]);
            pro.Price = Convert.ToInt32(Request["Price"]);
            pro.ItemAvailability=Request["radiobtn"];
            pro.ItemCategory = Request["radiobtnCategory"];

                if (CorrectTemp.DataAccess.DA.addItem(pro.ItemID, pro.ItemName, pro.ItemDescription, (int)pro.Stock, (int)pro.Discount, (int)pro.Price, pro.ItemAvailability, pro.ItemCategory))
                {
                    return RedirectToAction("../AddProducts/AddProducts");
                }
                else
                {
                    ModelState.AddModelError("", "There was a problem, try again!");
                }
            
            
            return View();
        }
        public ActionResult viewProducts() {
            using (var context = new BWAEntities())
            {
                var prod = context.Products.SqlQuery("SELECT * FROM dbo.Product").ToList();

                return View(prod);
            }
        }
        public ActionResult admin_Remove_Product(string id)
        {
            if (CorrectTemp.DataAccess.DA.remove_product(id) == true)
            {

                return RedirectToAction("../AddProducts/viewProducts");
            }
            else
            {
                ModelState.AddModelError("", "Something went wwrong. Try again!");
            }
            return View();
        }
    }
}