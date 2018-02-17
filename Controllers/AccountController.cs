using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorrectTemp.Models;
using System.Security.Authentication;

namespace BWA_CB006302.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult myAccount()
        {
            return View();
        }
        public ActionResult register()
        {

            return View();
        }
        [HttpPost]//post attribute
        public ActionResult register(userAccount user)
        {
            string username = Request["username"];
            string userPwd = Request["Password"];
            string userAdrs = Request["shippingAddress"];

            if (!(username== "," && userAdrs== "," && userPwd== ",")) {
                if (CorrectTemp.DataAccess.DA.AddUser(user.userID, user.username, user.shippingAddress, user.CardNumber, user.email, user.Password, user.userType))
                {
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.Message = "There was a problem, register again!";
                    return View("../Account/login");
                }
            }
            else
            {
                ViewBag.Message = "Please enter valid fields!";
                return View("../Account/login");
            }
            
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(userAccount user)
        {
            string username = Request["username"];
            string Password = Request["Password"];

            if (!(username == "" && Password == ""))
            {
                if (CorrectTemp.DataAccess.DA.adminIsValid(user.username, user.Password))
                {
                    loginSession();
                    return RedirectToAction("../Home/admin_Index");
                }
                else if (CorrectTemp.DataAccess.DA.UserIsValid(user.username, user.Password))
                {
                    loginSession();
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.Message = "Fields entered are incorrect. Try again!";
                    return View("../Account/login");
                }
            }
            else 
            {
                ViewBag.Message = "Please enter valid fields!";
                return View("../Account/login");

            }
        }
        public ActionResult loginSession()
        {
            string username = Request["username"];
            string password = Request["password"];
            bool currentUser;

            try
            {
                currentUser = CorrectTemp.DataAccess.DA.UserIsValid(username, password);
                do
                {
                    Session.Add("userSession", username);
                    return new HttpStatusCodeResult(200);
                } while (currentUser == true);
            }
            catch (InvalidCredentialException)
            {
                return new HttpStatusCodeResult(480);
            }
        }
        public ActionResult resetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resetPwdAction()
        {
            string username = Request["username"];
            string password1 = Request["pwd1"];
            string password2 = Request["pwd"];

            if (!(username=="" && password2==""))
            {
                if (password1.Equals(password2))
                {
                    using (var context = new BWAEntities())
                    {
                        context.Database.ExecuteSqlCommand("update Account set password='" + password2 + "' where username='" + username + "'");
                        context.SaveChanges();
                        //ViewBag.Message = "Successfully updated!";
                        //TempData["Success"] = "Successfully updated!";
                        return RedirectToAction("../Account/login");
                    }
                }
                else
                {
                    ViewBag.Message = "Passwords does not match!";
                    return View("resetPassword");
                }
            }
            else
            {
                ViewBag.Message = "Please enter valid fields!";
                return View("resetPassword");
            }
            
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return Redirect("/");
        }
    }
}