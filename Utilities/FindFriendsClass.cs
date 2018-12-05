using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class FindFriendsClass
    {
        public String userEmail { get; set; }
        public String friendEmail { get; set; }
        public String name { get; set; }
        public FindFriendsClass() {
        }
        public FindFriendsClass(String email) {
            this.userEmail = email;
        }

        public FindFriendsClass(String friEmail, String tempName) {
            this.friendEmail = friEmail;
            this.name = tempName;
        }

        public List<FindFriendsClass> GetFriendMethod(String email)
        {
            List<FindFriendsClass> FriendsArray = new List<FindFriendsClass>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetFriends";
            objCommand.Parameters.AddWithValue("@userEmail", email);

            DataSet FriendsDS = objDB.GetDataSetUsingCmdObj(objCommand);

            if (FriendsDS.Tables[0].Rows.Count != 0 && FriendsDS != null && FriendsDS.Tables.Count != 0)
            {
                FindFriendsClass FFC;
                for (int i = 0; i < FriendsDS.Tables[0].Rows.Count; i++)
                {
                    FFC = new FindFriendsClass();
                    FFC.name = FriendsDS.Tables[0].Rows[i]["name"].ToString();
                    FFC.userEmail = FriendsDS.Tables[0].Rows[i]["friendEmail"].ToString();
                    FriendsArray.Add(FFC);
                }
                return FriendsArray;
            }
            else {
                return null;
            }
        }

    }
}
