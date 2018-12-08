using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace TPWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FindFriends")]
    public class FindFriendsController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        [HttpPost]
        [Route("FindFriendsDS")]
        public List<FindFriendsClass> Post([FromBody] FindFriendsClass ffObject)
        {
            FindFriendsClass FFClassObject = new FindFriendsClass();
            return FFClassObject.GetFriendMethod(ffObject.userEmail.ToString());
        }

        [HttpGet]
        [Route("FindUsersByName")]
        public DataSet FindUsersByName(String name)
        {
            DataSet myUsers = new DataSet();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByName";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theName", name);

            myUsers = objDB.GetDataSetUsingCmdObj(objCommand);

            return myUsers;
        }

        [HttpGet]
        [Route("FindUsersByLocation")]
        public DataSet FindUsersByLocation(String city, String state)
        {
            DataSet myUsers = new DataSet();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByLocation";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theState", state);
            objCommand.Parameters.AddWithValue("@theCity", city);

            myUsers = objDB.GetDataSetUsingCmdObj(objCommand);

            return myUsers;
        }

        [HttpGet]
        [Route("FindUsersByOrganization")]
        public DataSet FindUsersByOrganization(String organization)
        {
            DataSet myUsers = new DataSet();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByOrganization";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theOrganization", organization);

            myUsers = objDB.GetDataSetUsingCmdObj(objCommand);

            return myUsers;
        }

        [HttpGet]
        [Route("GetFriendsArray")]
        public Friend[] GetFriends(String requestingUsername, String requestedUsername, String verificationToken)
        {
            if (verificationToken == "goodPassword")
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPGetFriendsMore";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@userEmail", requestedUsername);

                DataSet myFriends = objDB.GetDataSetUsingCmdObj(objCommand);
                if(myFriends.Tables[0].Rows.Count > 0)
                {
                    Friend[] arrFriends = new Friend[myFriends.Tables[0].Rows.Count];
                    for(int i = 0; i < myFriends.Tables[0].Rows.Count; i++)
                    {
                        Friend temp = new Friend();
                        temp.FriendID = int.Parse(myFriends.Tables[0].Rows[i][0].ToString());
                        temp.UserEmail = myFriends.Tables[0].Rows[i][1].ToString();
                        temp.FriendEmail = myFriends.Tables[0].Rows[i][2].ToString();
                        temp.FriendDate = DateTime.Parse(myFriends.Tables[0].Rows[i][3].ToString());
                        arrFriends[i] = temp;
                    }
                    return arrFriends;
                }
            }
            return null;
        }
    }
}