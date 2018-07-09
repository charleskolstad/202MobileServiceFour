using _202MobileServiceFour_Core;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _202MobileServiceFour_Web.Controllers
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

        public ActionResult OrderApp(string page)
        {
            ViewData["Page"] = (string.IsNullOrEmpty(page)) ? "ClientRegister" : page;
            ViewBag.typeList = WebTypeList();
            return View();
        }

        public ActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientRegister(UserInfo user)
        {
            string errorMessage = UserManager.ValidateClient(user);
            return RedirectToAction("OrderApp", new { page = "BusinessInfo" });
        }

        public ActionResult BusinessInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BusinessInfo(Business business)
        {
            return RedirectToAction("OrderApp", new { page = "Features" });
        }

        public ActionResult Features()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Features(Features features)
        {
            return RedirectToAction("OrderApp", new { page = "OrderResult" });
        }

        public ActionResult OrderResult()
        {
            return View();
        }

        private List<string> WebTypeList()
        {
            return new List<string>() { "A", "B", "C" };
        }
    }
}