using _202MobileServiceFour_Data;
using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Core
{
    public class AppRequestManager
    {
        public static List<AppRequest> AppRequestsGetAll(bool isUnitTest = false)
        {
            List<AppRequest> appRequests = new List<AppRequest>();

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                DataTable requestTable = sprocCalls.AppRequestsGetAll();
                AppRequest request;

                foreach (DataRow row in requestTable.Rows)
                {
                    request = new AppRequest();
                    request.Active = Convert.ToBoolean(row["Active"]);
                    request.AppBusiness = sprocCalls.GetBusinessByID(Convert.ToInt32(row["BusinessID"]));
                    request.AppRequestID = Convert.ToInt32(row["AppRequestID"]);
                    request.DateRequested = Convert.ToDateTime(row["DateRequested"]);
                    request.DevStatus = (row["DevStatus"] != DBNull.Value) ? row["DevStatus"].ToString() : "";

                    appRequests.Add(request);
                } 
            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return appRequests;
        }

        public static void AppRequestUpdate(AppRequest request, out string errorMessage, bool isUnitTest = false)
        {
            try
            {                
                errorMessage = ValidateRequest(request);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                    if (sprocCalls.AppRequestUpdate(request) == false)
                        errorMessage = "Error updating request.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }
        }

        private static string ValidateRequest(AppRequest request)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(request.DevStatus))
                errorMessage = "Request must have a status before updating.";

            return errorMessage;
        }
    }
}
