using _202MobileServiceFour_Data;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Core
{
    public class BusinessManager
    {
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
