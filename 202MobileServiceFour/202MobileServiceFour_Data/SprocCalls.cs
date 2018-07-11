using _202MobileServiceFour_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202MobileServiceFour_Data
{
    public class SprocCalls : ISprocCalls
    {
        #region userinfo
        public override DataTable UserInfoGetAll()
        {
            return DBCommands.AdapterFill("p_UserInfo_GetAll");
        }

        public override UserInfo UserInfoGetByUser(string userName)
        {
            DBCommands.PopulateParams("@UserName", userName);

            return (UserInfo)DBCommands.DataReader("p_UserInfo_GetByUser", DBCommands.ObjectTypes.UserInfo);
        }

        public override bool UserInfoUpdate(UserInfo user)
        {
            DBCommands.PopulateParams("@UserName", user.UserName);
            DBCommands.PopulateParams("@ProfileImage", user.ProfileImage);
            DBCommands.PopulateParams("@Phone", user.Phone);
            DBCommands.PopulateParams("@Name", user.Name);
            DBCommands.PopulateParams("@AddBusiness", user.AddBusiness);
            DBCommands.PopulateParams("@GroupUsers", MapGroupListToTable(user.GroupUsers));

            return DBCommands.ExecuteNonQuery("p_UserInfo_Update");
        }
        #endregion

        #region group users
        public override DataTable GroupUsersGetByUserName(string userName)
        {
            DBCommands.PopulateParams("@UserName", userName);

            return DBCommands.AdapterFill("p_GroupUsers_GetByUserName");
        }

        public override DataTable UserGroupsGetActive()
        {
            return DBCommands.AdapterFill("p_UserGroup_GetActive");
        }
        #endregion

        #region features
        public override DataTable FeaturesGetAll()
        {
            return DBCommands.AdapterFill("p_Feature_GetActive");
        }

        public override int FeaturesInsert(Features feature)
        {
            DBCommands.PopulateParams("@FeatureName", feature.FeatureName);
            DBCommands.PopulateParams("@MainFeature", feature.MainFeature);
            DBCommands.PopulateParams("@FeatureDescription", feature.FeatureDescription);

            int id = -1;
            Int32.TryParse(DBCommands.ExecuteScalar("p_Feature_Insert"), out id);

            return id;
        }

        public override bool FeaturesUpdate(Features feature)
        {
            DBCommands.PopulateParams("@FeatureID", feature.FeatureID);
            DBCommands.PopulateParams("@FeatureName", feature.FeatureName);
            DBCommands.PopulateParams("@MainFeature", feature.MainFeature);
            DBCommands.PopulateParams("@FeatureDescription", feature.FeatureDescription);

            return DBCommands.ExecuteNonQuery("p_Feature_Update");
        }

        public override bool FeatureDelete(int featureID)
        {
            DBCommands.PopulateParams("@FeatureID", featureID);

            return DBCommands.ExecuteNonQuery("p_Feature_Delete");
        }

        public override Features FeatureGetByID(int featureID)
        {
            DBCommands.PopulateParams("@FeatureID", featureID);

            return (Features)DBCommands.DataReader("p_Feature_GetByID", DBCommands.ObjectTypes.Features);
        }
        #endregion

        #region business
        public override Business GetBusinessByUser(string userName)
        {
            DBCommands.PopulateParams("@UserName", userName);

            return (Business)DBCommands.DataReader("p_Business_GetByUser", DBCommands.ObjectTypes.Business);
        }

        public override bool BusinessUpdate(Business business)
        {
            DBCommands.PopulateParams("@BusinessID", business.BusinessID);
            DBCommands.PopulateParams("@BusinessName", business.BusinessName);
            DBCommands.PopulateParams("@BusinessEmail", business.BusinessEmail);
            DBCommands.PopulateParams("@BusinessAddress", business.BusinessAddress);
            DBCommands.PopulateParams("@BusinessHoursStart", business.BusinessHoursStart);
            DBCommands.PopulateParams("@BusinessHoursEnd", business.BusinessHoursEnd);
            DBCommands.PopulateParams("@WebsiteUrl", business.WebsiteUrl);
            DBCommands.PopulateParams("@FacebookUrl", business.FacebookUrl);
            DBCommands.PopulateParams("@ImageGalleryUrl", business.ImageGalleryUrl);
            DBCommands.PopulateParams("@Other", business.Other);
            DBCommands.PopulateParams("@TypeOfBusiness", business.TypeOfBusiness);
            DBCommands.PopulateParams("@AppLink", business.AppLink);
            DBCommands.PopulateParams("@IsPublic", business.IsPublic);
            DBCommands.PopulateParams("@AppStatus", business.AppStatus);
            DBCommands.PopulateParams("@UserName", business.user.UserName);

            return DBCommands.ExecuteNonQuery("p_Business_Update");
        }

        public override DataTable BusinessTypesAll()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region requested feature


        public override DataTable BusinessRequestedFeature(int businessID)
        {
            DBCommands.PopulateParams("@BusinessID", businessID);

            return DBCommands.AdapterFill("p_FeatureRequested_GetByBusinessID");
        }

        public override bool RequestedFeatureUpdate(FeatureRequested feature)
        {
            DBCommands.PopulateParams("@FeatureRequestedID", feature.FeatureRequestedID);
            DBCommands.PopulateParams("@AppRequestID", feature.AppRequestID);
            DBCommands.PopulateParams("@FeatureID", feature.RequestedFeature.FeatureID);
            DBCommands.PopulateParams("@DateRequested", feature.DateRequested);
            DBCommands.PopulateParams("@DevStatus", feature.DevStatus);
            DBCommands.PopulateParams("@AssignedTo", feature.AssignedTo);
            DBCommands.PopulateParams("@Active", feature.Active);

            return DBCommands.ExecuteNonQuery("p_FeatureRequested_Update");
        }
        #endregion
    }

    public class FakeSprocCalls : ISprocCalls
    {
        #region userinfo
        public override DataTable UserInfoGetAll()
        {
            DataTable users = new DataTable();
            users.Columns.Add("UserInfoID");
            users.Columns.Add("UserName");
            users.Columns.Add("Email");
            users.Columns.Add("ProfileImage");

            DataRow row = users.NewRow();
            row["UserInfoID"] = 1;
            row["UserName"] = "TestUser";
            row["Email"] = "test@Test.com";
            row["ProfileImage"] = "Image";
            users.Rows.Add(row);

            return users;
        }

        public override UserInfo UserInfoGetByUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            else
            {
                UserInfo user = new UserInfo();
                user.UserName = userName;

                return user;
            }
        }

        public override bool UserInfoUpdate(UserInfo user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                return false;
            else
            {
                List<UserGroups> groups = new List<UserGroups>();
                UserGroups g = new UserGroups() { UserGroupID = 1, GroupName = "Admin" };
                groups.Add(g);

                DataTable groupsTable = MapGroupListToTable(groups);

                return (groupsTable.Rows.Count == 1);
            }
        }
        #endregion

        #region group users
        public override DataTable GroupUsersGetByUserName(string userName)
        {
            DataTable groups = new DataTable();
            groups.Columns.Add("GroupUsersID");
            groups.Columns.Add("UserName");
            groups.Columns.Add("UserGroupID");
            groups.Columns.Add("GroupName");
            groups.Columns.Add("GroupLevel");
            groups.Columns.Add("Active");

            if (string.IsNullOrEmpty(userName) == false)
            {
                DataRow row = groups.NewRow();
                row["GroupUsersID"] = 1;
                row["UserName"] = "TestUser";
                row["UserGroupID"] = 1;
                row["GroupName"] = "TestGroup";
                row["GroupLevel"] = 1;
                row["Active"] = true;
                groups.Rows.Add(row);
            }

            return groups;
        }

        public override DataTable UserGroupsGetActive()
        {
            DataTable groups = new DataTable();
            groups.Columns.Add("UserGroupID");
            groups.Columns.Add("GroupName");
            groups.Columns.Add("GroupLevel");
            groups.Columns.Add("Active");
            DataRow row = groups.NewRow();

            row["UserGroupID"] = 1;
            row["GroupName"] = "TestGroup";
            row["GroupLevel"] = 1;
            row["Active"] = true;
            groups.Rows.Add(row);

            return groups;
        }
        #endregion

        #region features
        public override DataTable FeaturesGetAll()
        {
            DataTable featureTable = new DataTable();
            featureTable.Columns.Add("FeatureID");
            featureTable.Columns.Add("FeatureName");
            featureTable.Columns.Add("MainFeature");
            featureTable.Columns.Add("FeatureDescription");
            featureTable.Columns.Add("Active");

            DataRow row = featureTable.NewRow();
            row["FeatureID"] = 1;
            row["FeatureName"] = "Name";
            row["MainFeature"] = true;
            row["FeatureDescription"] = "Description";
            row["Active"] = true;
            featureTable.Rows.Add(row);

            return featureTable;
        }

        public override int FeaturesInsert(Features feature)
        {
            if (string.IsNullOrEmpty(feature.FeatureName))
                return -1;

            return 1;
        }

        public override bool FeaturesUpdate(Features feature)
        {
            if (string.IsNullOrEmpty(feature.FeatureName))
                return false;

            return true;
        }

        public override bool FeatureDelete(int featureID)
        {
            if (featureID <= 0)
                return false;

            return true;
        }

        public override Features FeatureGetByID(int featureID)
        {
            if (featureID <= 0)
                return null;

            return new Features();
        }
        #endregion

        #region business
        public override Business GetBusinessByUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            return new Business();
        }

        public override bool BusinessUpdate(Business business)
        {
            if (string.IsNullOrEmpty(business.BusinessName))
                return false;

            return true;
        }

        public override DataTable BusinessTypesAll()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region requested features
        public override DataTable BusinessRequestedFeature(int businessID)
        {
            DataTable requestedFeatures = new DataTable();
            requestedFeatures.Columns.Add("FeatureRequestedID");
            requestedFeatures.Columns.Add("AppRequestID");
            requestedFeatures.Columns.Add("FeatureID");
            requestedFeatures.Columns.Add("DateRequested");
            requestedFeatures.Columns.Add("DevStatus");
            requestedFeatures.Columns.Add("AssignedTo");
            requestedFeatures.Columns.Add("Active");

            if (businessID > 0)
            {
                DataRow row = requestedFeatures.NewRow();
                row["FeatureRequestedID"] = 1;
                row["AppRequestID"] = 1;
                row["FeatureID"] = 1;
                row["DateRequested"] = DateTime.Now;
                row["DevStatus"] = "";
                row["AssignedTo"] = "cpkolsta";
                row["Active"] = true;
                requestedFeatures.Rows.Add(row);
            }

            return requestedFeatures;
        }

        public override bool RequestedFeatureUpdate(FeatureRequested feature)
        {
            if (feature.AppRequestID <= 0)
                return false;

            return true;
        }
        #endregion
    }

    public abstract class ISprocCalls
    {
        #region userinfo
        public abstract DataTable UserInfoGetAll();
        public abstract UserInfo UserInfoGetByUser(string userName);
        public abstract bool UserInfoUpdate(UserInfo user);
        #endregion

        #region group users
        public abstract DataTable GroupUsersGetByUserName(string userName);
        public abstract DataTable UserGroupsGetActive();
        #endregion

        #region features
        public abstract DataTable FeaturesGetAll();
        public abstract int FeaturesInsert(Features feature);
        public abstract bool FeaturesUpdate(Features feature);
        public abstract bool FeatureDelete(int featureID);
        public abstract Features FeatureGetByID(int featureID);
        #endregion

        #region business
        public abstract Business GetBusinessByUser(string userName);
        public abstract bool BusinessUpdate(Business business);
        public abstract DataTable BusinessTypesAll();
        #endregion

        #region requested features
        public abstract DataTable BusinessRequestedFeature(int businessID);
        public abstract bool RequestedFeatureUpdate(FeatureRequested feature);
        #endregion

        public DataTable MapGroupListToTable(List<UserGroups> groups)
        {
            DataTable groupTable = new DataTable();
            groupTable.Columns.Add("UserGroupID");
            groupTable.Columns.Add("Active");

            if (groups.Count > 0)
            {
                DataRow row;

                foreach (UserGroups g in groups)
                {
                    row = groupTable.NewRow();
                    row["UserGroupID"] = g.UserGroupID;
                    row["Active"] = g.Active;

                    groupTable.Rows.Add(row);
                }
            }

            return groupTable;
        }
    }
}
