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
    [Authorize]
    public class AppsController : Controller
    {
        [AllowAnonymous]
        public ActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult ClientLogin()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult ClientLogin(LoginModel model)
        {
            Provider provider = new Provider();

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("BusinessInfo");
                }

                ModelState.AddModelError("", "Invalid username and password.");
            }

            return View();
        }
        
        public ActionResult BusinessInfo()
        {
            ViewBag.typeList = WebTypeList();
            Business business = BusinessManager.BusinessGetByUser(User.Identity.Name);

            return View(business);
        }

        [HttpPost]
        public ActionResult BusinessInfo(Business business)
        {
            string errorMessage = string.Empty;
            business.user = UserManager.GetUserByName(User.Identity.Name);
            BusinessManager.BusinessUpdate(business, out errorMessage);

            if (string.IsNullOrEmpty(errorMessage))
                return RedirectToAction("Features", new { businessID = business.BusinessID});
            else
            {
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.typeList = WebTypeList();
                return View(business);
            }
        }

        public ActionResult Features(int businessID)
        {
            List<FeatureRequested> model = FeatureManager.RequestedFeaturesByBusiness(businessID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Features(List<FeatureRequested> features)
        {
            string errorMessage = string.Empty;
            FeatureManager.RequestFeatureUpdate(features, out errorMessage);

            TempData["MyRequests"] = features;
            return RedirectToAction("OrderResult");
        }

        public ActionResult OrderResult()
        {
            BusinessManager.SendAlertToWorkers();
            List<FeatureRequested> requestedFeatures = TempData["MyRequests"] as List<FeatureRequested>;
            return View(requestedFeatures);
        }

        private List<string> WebTypeList()
        {
            return new List<string>() { "A", "B", "C" };
        }
    }
}