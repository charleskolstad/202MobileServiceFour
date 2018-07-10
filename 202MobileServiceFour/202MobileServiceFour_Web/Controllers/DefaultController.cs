using _202MobileServiceFour_Core;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        public ActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClientRegister(UserInfo user)
        {
            Provider provider = new Provider();
            user.AddBusiness = true;
            string errorMessage = string.Empty;
            UserManager.InsertClient(user, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
            {
                if (Membership.ValidateUser(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("BusinessInfo");
                }
                else
                    errorMessage = "Error validating account.";
            }

            ViewBag.ErrorMessage = errorMessage;
            return View(user);           
        }

        [Authorize]
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