using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ARM_MVC.Models;


namespace Project_ARM_MVC.Controllers
{
    public class HomeController : Controller
    {
        ARMPROJECT1Entities1 db = new ARMPROJECT1Entities1();
        public ActionResult Index()
        {
            return View();
        }
        ////////////////////////////////////////////
        public ActionResult LayoutMarketing()
        {
            return View();
        }
        public ActionResult Layoutarm1()
        {
            return View();
        }
        public ActionResult Layoutarm2()
        {
            return View();
        }
        ///////////////////////////////////////////
        public ActionResult UserView()
        {
            return View();
        }
        ////////////////////////////////////////////
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        ////////////////////////////////////////////
        public ActionResult IndexAdmin()
        {
            

            return View();
        }

        ///////////////////////////////////////////
        public ActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(User data)
        {
            var User = db.User.Where(a => a.User_Name == data.User_Name
            && a.User_Pass == data.User_Pass).FirstOrDefault();


            if (User == null)
            {
                TempData["Erroruser"] = "Error";
                return View("LoginUser",User);
            }
            else
            {
                Session["LoginUser"] = User;
                  return RedirectToAction("Index123", "Users");
            }
            //if (User = null)
            //{
            //    Session["LoginUser"] = User;
            //    return RedirectToAction("Index123", "Users");
            //}
            //return View();
        }
        /////////////////////////////////////////
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(Admin data)
        {
            var admin = db.Admin.Where(a => a.Admin_Name == data.Admin_Name
            && a.Admin_Pass == data.Admin_Pass).FirstOrDefault();

            if (admin == null)
            {
                TempData["ErrorAdmin"] = "Error";
                return View("LoginAdmin",admin);
            }
            else
            {
                Session["admin"] = admin;
                return RedirectToAction("IndexAdmin", "Home");
            }
      
        }

        /////////////////////////////////////////////////
    }
}