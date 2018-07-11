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
    public class FeatureManager
    {
        public static List<Features> GetAllFeatures(bool allActive, bool isUnitTest = false)
        {
            List<Features> allFeatures = new List<Features>();

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                DataTable featureTable = sprocCalls.FeaturesGetAll();

                foreach (DataRow row in featureTable.Rows)
                {
                    Features feature = new Features();
                    feature.Active = allActive;
                    feature.FeatureDescription = (row["FeatureDescription"] != DBNull.Value) ? row["FeatureDescription"].ToString() : "";
                    feature.FeatureID = Convert.ToInt32(row["FeatureID"]);
                    feature.FeatureName = row["FeatureName"].ToString();
                    feature.MainFeature = Convert.ToBoolean(row["MainFeature"]);

                    allFeatures.Add(feature);
                }
            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return allFeatures;
        }

        public static int FeatureInsert(Features feature, out string errorMessage, bool isUnitTest = false)
        {
            int id = -1;

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                IEmailTools emailTools = AppTools.InitEmailTools(isUnitTest);
                errorMessage = ValidateFeature(feature);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    id = sprocCalls.FeaturesInsert(feature);
                    if (id <= 0)
                        errorMessage = "Error inserting new feature " + feature.FeatureName + ".";                    
                    //else
                    //    emailTools.SendToAll("New Feature created : " + feature.FeatureName + "<br />Description : " + feature.FeatureDescription);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }

            return id;
        }

        public static void FeatureUpdate(Features feature, out string errorMessage, bool isUnitTest = false)
        {
            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                errorMessage = ValidateFeature(feature);

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (sprocCalls.FeaturesUpdate(feature) == false)
                        errorMessage = "Error updating feature " + feature.FeatureName + ".";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }
        }

        public static void FeatureDelete(int featureID, out string errorMessage, bool isUnitTest = false)
        {
            try
            {
                errorMessage = string.Empty;

                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                if (sprocCalls.FeatureDelete(featureID) == false)
                    errorMessage = "Error deleting feature.";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }
        }

        public static Features FeatureGetByID(int featureID, bool IsUnitTest = false)
        {
            Features feature = null;

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(IsUnitTest);
                feature = sprocCalls.FeatureGetByID(featureID);
            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return feature;
        }

        private static string ValidateFeature(Features feature)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(feature.FeatureName))
                errorMessage += "Feature name is required.<br />";

            return errorMessage;
        }

        public static List<FeatureRequested> RequestedFeaturesByBusiness(int businessID, bool isUnitTest = false)
        {
            List<FeatureRequested> features = new List<FeatureRequested>();

            try
            {
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);
                DataTable featureTable = sprocCalls.BusinessRequestedFeature(businessID);
                FeatureRequested requestFeature;

                foreach (DataRow row in featureTable.Rows)
                {
                    requestFeature = new FeatureRequested();
                    requestFeature.Active = Convert.ToBoolean(row["Active"]);
                    requestFeature.AppRequestID = Convert.ToInt32(row["AppRequestID"]);
                    requestFeature.AssignedTo = (row["AssignedTo"] != DBNull.Value) ? UserManager.GetUserByName(row["AssignedTo"].ToString()) : null;
                    requestFeature.DateRequested = (row["DateRequested"] != DBNull.Value) ? Convert.ToDateTime(row["DateRequested"]) : (DateTime?)null;
                    requestFeature.DevStatus = (row["DevStatus"] != DBNull.Value) ? row["DevStatus"].ToString() : "";
                    requestFeature.FeatureRequestedID = Convert.ToInt32(row["FeatureRequestedID"]);
                    requestFeature.RequestedFeature = FeatureGetByID(Convert.ToInt32(row["FeatureID"]));

                    features.Add(requestFeature);
                }

            }
            catch (Exception ex)
            {
                DBCommands.RecordError(ex);
            }

            return features;
        }

        public static void RequestFeatureUpdate(List<FeatureRequested> features, out string errorMessage, bool isUnitTest = false)
        {
            try
            {
                errorMessage = string.Empty;
                ISprocCalls sprocCalls = AppTools.InitSprocCalls(isUnitTest);

                foreach (FeatureRequested request in features)
                {
                    if (sprocCalls.RequestedFeatureUpdate(request) == false)
                        errorMessage = "Error gathering all features";
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                DBCommands.RecordError(ex);
            }
        }
    }
}
