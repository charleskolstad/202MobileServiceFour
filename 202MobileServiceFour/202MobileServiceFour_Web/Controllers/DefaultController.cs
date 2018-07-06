using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _202MobileService_Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Directory()
        {
            return View();
        }

        public ActionResult OrderApp()
        {
            ViewData["Page"] = "ClientRegister";
            return View();
        }

        public ActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientRegister(UserInfo user)
        {
            ViewData["Page"] = "BusinessInfo";
            return RedirectToAction("BusinessInfo");
        }

        public ActionResult BusinessInfo()
        {
            return View();
        }

        public ActionResult Features()
        {
            return View();
        }

        public ActionResult OrderResult()
        {
            return View();
        }
    }
}