using _202MobileServiceFour_Data;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Core
{
    public class BusinessManager
    {
        public static List<Business> BusinessGetAll(bool isTest = false)
        {
            List<Business> businesses = new List<Business>();

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isTest);
                DataTable table = sprocCalls.BusinessGetAll();
                Business business;

                foreach (DataRow row in table.Rows)
                {
                    business = new Business();
                    business.AppLink = (row["AppLink"] != DBNull.Value) ? row["AppLink"].ToString() : "";
                    business.AppStatus = (row["AppStatus"] != DBNull.Value) ? row["AppStatus"].ToString() : "";
                    business.BusinessAddress = (row["BusinessAddress"] != DBNull.Value) ? row["BusinessAddress"].ToString() : "";
                    business.BusinessEmail = (row["BusinessEmail"] != DBNull.Value) ? row["BusinessEmail"].ToString() : "";
                    business.BusinessHoursEnd = (row["BusinessHoursEnd"] != DBNull.Value) ? row["BusinessHoursEnd"].ToString() : "";
                    business.BusinessHoursStart = (row["BusinessHoursStart"] != DBNull.Value) ? row["BusinessHoursStart"].ToString() : "";
                    business.BusinessID = Convert.ToInt32(row["BusinessID"]);
                    business.BusinessName = (row["BusinessName"] != DBNull.Value) ? row["BusinessName"].ToString() : "";
                    business.BusinessPhone = (row["BusinessPhone"] != DBNull.Value) ? row["BusinessPhone"].ToString() : "";
                    business.FacebookUrl = (row["FacebookUrl"] != DBNull.Value) ? row["FacebokUrl"].ToString() : "";
                    business.ImageGalleryUrl = (row["ImageGalleryUrl"] != DBNull.Value) ? row["ImageGalleryUrl"].ToString() : "";
                    business.IsPublic = Convert.ToBoolean(row["IsPublic"] != DBNull.Value) ? Convert.ToBoolean(row["IsPublic"]) : false;
                    business.Other = (row["Other"] != DBNull.Value) ? row["Other"].ToString() : "";
                    business.TypeOfBusiness = (row["TypeOfBusiness"] != DBNull.Value) ? row["TypeOfBusiness"].ToString() : "";
                    business.user = UserManager.GetUserByName(row["UserName"].ToString());
                    business.WebsiteUrl = (row["WebsiteUrl"] != DBNull.Value) ? row["WebsiteUrl"].ToString() : "";

                    businesses.Add(business);
                }

            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return businesses;
        }

        public static Business BusinessGetByUser(string userName, bool isTest = false)
        {
            Business business = new Business();
            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isTest);
                business = sprocCalls.GetBusinessByUser(userName);
                business.user = UserManager.GetUserByName(userName);
            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return business;
        }

        public static void BusinessUpdate(Business business, out string errorMessage, bool isTest = false)
        {
            try
            {
                errorMessage = ValidateBusiness(business);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    ISprocCalls sprocCalls = AppTools.InitSprocCalls(isTest);
                    if (sprocCalls.BusinessUpdate(business) == false)
                        errorMessage = "Error updating business.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }
        }

        public static void SendAlertToWorkers(bool isTest = false)
        {
            try
            {
                IEmailTools emailTools = AppTools.InitEmailTools(isTest);
                emailTools.SendToGroup(2, "A new request has been sent.");
                emailTools.SendToGroup(3, "A new request has been sent.");
            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }
        }

        private static string ValidateBusiness(Business business)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(business.BusinessName))
                errorMessage = "Business name is required.";

            if (string.IsNullOrEmpty(business.BusinessEmail) == false)
            {
                string emailRegEx = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                if (Regex.IsMatch(business.BusinessEmail, emailRegEx, RegexOptions.IgnoreCase) == false)
                    errorMessage += "Please provide valid email.<br />";
            }

            if (string.IsNullOrEmpty(business.BusinessPhone) == false)
            {
                string phoneRegEx = @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$";
                if (Regex.IsMatch(business.BusinessPhone, phoneRegEx, RegexOptions.IgnoreCase) == false)
                    errorMessage += "Please provide valid phone number.<br />";
            }

            return errorMessage;
        }
    }
}
