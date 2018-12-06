using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
   public class Profile
    {
        public String name { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String zip { get; set; }
        public String birthday { get; set; }

        public Profile() { }

        public String CheckRequestedUserPrivacySetting(String RequestedUserEmail) {

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_RequestedUserProfileInfoStatus";
            objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            return UserInfoDataSet.Tables[0].Rows[0]["ProfileInfoPrivacy"].ToString();

        }

        public List<Profile> RetreiveProfileInfo(String RequestingUser, String RequestedUserEmail, String privacySetting)
        {
            //Check if they are friends first 
            Boolean checkifFriends = CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
            if (checkifFriends == true) {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPRetrieveUserProfileInfo";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

                List<Profile> ProfileArray = new List<Profile>();

                Profile tempProfile = new Profile();
                tempProfile.name = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
                tempProfile.address = UserInfoDataSet.Tables[0].Rows[0]["address"].ToString();
                tempProfile.city = UserInfoDataSet.Tables[0].Rows[0]["city"].ToString();
                tempProfile.zip = UserInfoDataSet.Tables[0].Rows[0]["zip"].ToString();
                tempProfile.birthday = UserInfoDataSet.Tables[0].Rows[0]["birthday"].ToString();
                ProfileArray.Add(tempProfile);
                return ProfileArray;
            }

            List<Profile> tempArray = new List<Profile>();
            if (privacySetting == "Public")
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPRetrieveUserProfileInfo";
                objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

                List<Profile> ProfileArray = new List<Profile>();

                Profile tempProfile = new Profile();
                tempProfile.name = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
                tempProfile.address = UserInfoDataSet.Tables[0].Rows[0]["address"].ToString();
                tempProfile.city = UserInfoDataSet.Tables[0].Rows[0]["city"].ToString();
                tempProfile.zip = UserInfoDataSet.Tables[0].Rows[0]["zip"].ToString();
                tempProfile.birthday = UserInfoDataSet.Tables[0].Rows[0]["birthday"].ToString();
                ProfileArray.Add(tempProfile);
                return ProfileArray;
            }
            if (privacySetting == "Friends")
            {
                //Check if these 2 users are friends
                Boolean flag = CheckIf2UsersAreFriends(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TPRetrieveUserProfileInfo";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                    DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

                    List<Profile> ProfileArray = new List<Profile>();

                    Profile tempProfile = new Profile();
                    tempProfile.name = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
                    tempProfile.address = UserInfoDataSet.Tables[0].Rows[0]["address"].ToString();
                    tempProfile.city = UserInfoDataSet.Tables[0].Rows[0]["city"].ToString();
                    tempProfile.zip = UserInfoDataSet.Tables[0].Rows[0]["zip"].ToString();
                    tempProfile.birthday = UserInfoDataSet.Tables[0].Rows[0]["birthday"].ToString();
                    ProfileArray.Add(tempProfile);
                    return ProfileArray;
                }
                else {
                    return tempArray;
                }
            }
            if (privacySetting == "FOF")
            {
                //Check if Friends of Friends

                Boolean flag = CheckIfFOF(RequestingUser, RequestedUserEmail);
                if (flag == true)
                {
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TPRetrieveUserProfileInfo";
                    objCommand.Parameters.AddWithValue("@Email", RequestedUserEmail);

                    DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

                    List<Profile> ProfileArray = new List<Profile>();

                    Profile tempProfile = new Profile();
                    tempProfile.name = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
                    tempProfile.address = UserInfoDataSet.Tables[0].Rows[0]["address"].ToString();
                    tempProfile.city = UserInfoDataSet.Tables[0].Rows[0]["city"].ToString();
                    tempProfile.zip = UserInfoDataSet.Tables[0].Rows[0]["zip"].ToString();
                    tempProfile.birthday = UserInfoDataSet.Tables[0].Rows[0]["birthday"].ToString();
                    ProfileArray.Add(tempProfile);
                    return ProfileArray;
                }
                else {
                    return tempArray;
                }
            }
            else {
                return tempArray;
            }
        }
        //End of RetrieveProfileInfo

        public Boolean CheckIf2UsersAreFriends(String RequestingUser, String RequestedUserEmail) {

            //Check if RequestingUser is Friend of RequestedUserEmail

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_CheckIfFriends";
            objCommand.Parameters.AddWithValue("@RequestingUser", RequestingUser);
            objCommand.Parameters.AddWithValue("@RequestedUserEmail", RequestedUserEmail);
            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

            if (UserInfoDataSet != null && UserInfoDataSet.Tables.Count != 0 && UserInfoDataSet.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            else {
                return false;
            }

        }

        public Boolean CheckIfFOF(String RequestingUser, String RequestedUserEmail) {
            //Checks if they have mutual Friend and if they do they are friend of friend

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_CheckIfFOF";
            objCommand.Parameters.AddWithValue("@RequestingUser", RequestingUser);
            objCommand.Parameters.AddWithValue("@RequestedUser", RequestedUserEmail);
            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

            if (UserInfoDataSet != null && UserInfoDataSet.Tables.Count != 0 && UserInfoDataSet.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
