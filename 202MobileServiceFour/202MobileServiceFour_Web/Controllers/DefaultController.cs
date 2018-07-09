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
            ViewBag.ErrorMessage = TempData["errorMessage"];
            return View();
        }

        public ActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientRegister(UserInfo user)
        {
            user.GroupUsers = UserManager.GroupsGetAll();
            user.GroupUsers.Where(g => g.GroupLevel == 0).FirstOrDefault().Active = true;
            string errorMessage = UserManager.ValidateClient(user);

            if (string.IsNullOrEmpty(errorMessage))
                return RedirectToAction("OrderApp", new { page = "BusinessInfo" });
            else
            {
                TempData["errorMessage"] = errorMessage;
                return RedirectToAction("OrderApp", new { page = "ClientRegister" });
            }
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