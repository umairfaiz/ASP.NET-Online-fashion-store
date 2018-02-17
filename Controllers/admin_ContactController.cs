using CorrectTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BWA_CB006302.Controllers
{
    public class admin_ContactController : Controller
    {
        // GET: admin_Contact
        public ActionResult admin_Contact()
        {
            return View();
        }
        public ActionResult admin_Reply()
        {
            ViewBag.Title = "admin_Reply";
            return View();
        }

        [HttpPost]
        public ActionResult admin_Reply_msg(Message msg)
        {
            if (CorrectTemp.DataAccess.DA.admin_Message(msg.msgID, msg.username, msg.subject, msg.messageDetail, msg.reciever))
            {
                return RedirectToAction("../Home/admin_Index");
            }
            else
            {
                ModelState.AddModelError("", "Message sending failed. Try again!");
            }
            return View();
        }
        public ActionResult admin_Inbox()
        {
            var user = Session["userSession"];
            using (var context = new BWAEntities())
            {
                var message = context.Messages.SqlQuery("SELECT * FROM dbo.Message where reciever='"+user+"'").ToList();

                return View(message);
            }
        }
        public ActionResult admin_Remove_mail(int id)
        {
            if (CorrectTemp.DataAccess.DA.remove_mail(id) == true)
            {

                return RedirectToAction("../admin_Contact/admin_Inbox");
            }
            else
            {
                ModelState.AddModelError("", "Something went wwrong. Try again!");
            }
            return View();

        }
    }
}