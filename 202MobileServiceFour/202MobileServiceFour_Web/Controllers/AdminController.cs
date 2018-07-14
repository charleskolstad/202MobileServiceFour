using _202MobileServiceFour_Core;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _202MobileServiceFour_Web.Controllers
{    
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Provider provider = new Provider();
            UserInfo model = UserManager.GetUserByName(User.Identity.Name);
            return View(model);
        }

        #region login methods
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Provider provider = new Provider();
            return LoginStart(returnUrl);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            Provider provider = new Provider();

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, false);
                    return ManageReturnUrl(returnUrl);
                }

                ModelState.AddModelError("", "Invalid username and password.");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult RecoverPassword(string returnUrl)
        {
            return LoginStart(returnUrl);
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult RecoverPassword(RecoverModel recover, string returnUrl)
        {
            Provider provider = new Provider();

            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                UserManager.RecoverPassword(recover, out errorMessage);

                if (string.IsNullOrEmpty(errorMessage))
                    return ManageReturnUrl(returnUrl);

                ModelState.AddModelError("", errorMessage);
            }

            return View();
        }

        public ActionResult ManageAccount()
        {
            Provider provider = new Provider();

            UpdateAccount account = new UpdateAccount();
            account.UserName = Membership.GetUser(User.Identity.Name).UserName;

            return View(account);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ManageAccount(UpdateAccount account, string returnUrl)
        {
            Provider provider = new Provider();

            if (ModelState.IsValid)
            {
                string errorMessage = string.Empty;
                UserManager.UpdatePassword(account, out errorMessage);

                if (string.IsNullOrEmpty(errorMessage))
                    return ManageReturnUrl(returnUrl);

                ModelState.AddModelError("", errorMessage);
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }

        private ActionResult LoginStart(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return ManageReturnUrl(returnUrl);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ActionResult ManageReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return Redirect("Index");
        }
        #endregion

        #region partial views
        public ActionResult AllUsers()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult AllFeatures()
        {
            return View();
        }

        public ActionResult AppRequests()
        {
            return View();
        }

        public ActionResult Businesses()
        {
            return View();
        }
        #endregion

        #region angular methods
        public JsonResult GetAllUsers()
        {
            List<UserInfo> allUsers = UserManager.GetAllUsers();
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroups()
        {
            List<UserGroups> groups = UserManager.GroupsGetAll();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public string SaveUser(UserInfo user)
        {
            string errorMessage = string.Empty;

            if (user.UserInfoID > 0)
                UserManager.UpdateUser(user, out errorMessage);
            else
                UserManager.InsertUser(user, out errorMessage);

            return errorMessage;
        }

        public string DeleteUser(string delUser)
        {
            if (UserManager.DeleteUser(delUser))
                return string.Empty;

            return "Error deleting user.";
        }

        public JsonResult GetUserByName()
        {
            UserInfo userInfo = UserManager.GetUserByName(User.Identity.Name);
            return Json(userInfo, JsonRequestBehavior.AllowGet);
        }

        public string UpdateProfile(UserInfo user)
        {
            string errorMessage = string.Empty;
            user.ProfileImage = user.ProfileImage.Replace("../../..", "~");
            UserManager.UpdateUser(user, out errorMessage);

            return errorMessage;
        }

        public string SaveProfileImage(string newImage)
        {
            string errorMessage = string.Empty;
            UserInfo userInfo = UserManager.GetUserByName(User.Identity.Name);

            string fileName = "~/Assets/IMG/UploadIMG/" + Guid.NewGuid().ToString() + ".png";
            byte[] data = Convert.FromBase64String(newImage);
            var fileStream = new FileStream(Server.MapPath(fileName), FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
            userInfo.ProfileImage = fileName;

            UserManager.UpdateUser(userInfo, out errorMessage);
            return errorMessage;
        }

        public JsonResult GetAllFeatures()
        {
            List<Features> allFeatures = FeatureManager.GetAllFeatures(true);
            return Json(allFeatures, JsonRequestBehavior.AllowGet);
        }

        public string SaveFeature(Features feature)
        {
            string errorMessage = string.Empty;
            FeatureManager.FeatureInsert(feature, out errorMessage);

            return errorMessage;
        }

        public string UpdateFeature(Features feature)
        {
            string errorMessage = string.Empty;
            FeatureManager.FeatureUpdate(feature, out errorMessage);

            return errorMessage;
        }

        public string DeleteFeature(int featureID)
        {
            string errorMessage = string.Empty;
            FeatureManager.FeatureDelete(featureID, out errorMessage);

            return errorMessage;
        }

        public string AppRequestSave(AppRequest request)
        {
            string errorMessage = string.Empty;

            AppRequestManager.AppRequestUpdate(request, out errorMessage);
            return errorMessage;
        }

        public JsonResult AppRequestGetAll()
        {
            List<AppRequest> requests = AppRequestManager.AppRequestsGetAll();
            return Json(requests, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllBusinesses()
        {
            List<Business> businesses = BusinessManager.BusinessGetAll();
            return Json(businesses, JsonRequestBehavior.AllowGet);
        }

        public string UpdateBusiness(Business business)
        {
            string errorMessage = string.Empty;

            BusinessManager.BusinessUpdate(business, out errorMessage);
            return errorMessage;
        }
        #endregion
    }
}