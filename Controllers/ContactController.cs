using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorrectTemp.Models;

namespace BWA_CB006302.Controllers
{
    public class ContactController : Controller
    {
        
        public ActionResult Contact()
        {       
            return View();
        }
        [HttpPost]
        public ActionResult user_Feedback(Message msg)
        {
            string username = Request["username"];
            string subject = Request["subject"];
            string message = Request["messageDetail"];

            if (!(username == "" && subject == "" && message == ""))
            {
                if (CorrectTemp.DataAccess.DA.message_portal(msg.msgID, msg.username, msg.subject, msg.messageDetail, msg.reciever))
                {
                    return RedirectToAction("../Home/Index");
                }
                else
                {
                    ViewBag.Message = "Fields entered are incorrect. Try again!";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Please enter valid fields!";
                return View("../Contact/Contact");
            }

                
            
        }

        public ActionResult view_Inbox()
        {
            var user = Session["userSession"];
            using (var context = new BWAEntities())
            {
                var message = context.Messages.SqlQuery("SELECT * FROM dbo.Message where reciever='" + user+"'").ToList();

                return View(message);
            }
        }
        public ActionResult remove_mail(int id)
        {
            if (CorrectTemp.DataAccess.DA.remove_mail(id)==true)    
            {

                return RedirectToAction("../Contact/view_Inbox");
            }
            else
            {
                ModelState.AddModelError("", "Something went wwrong. Try again!");
            }
            return View();

        }

    }
}