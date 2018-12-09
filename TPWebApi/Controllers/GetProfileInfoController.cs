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
    [Route("api/GetProfileInfo")]
    public class GetProfileInfoController : Controller
    {
        [HttpPost]
        [Route("GetProfileInfoMethod")]
        public List<Profile> Post([FromBody] ProfileRequest RequestObject)
        {
            Profile ProfileObj = new Profile();
            //Check token
            if (RequestObject.Token == "1234")
            {
                //Check Requested User Profile Info Privacy Setting

                String settingRetrieved = ProfileObj.CheckRequestedUserPrivacySetting(RequestObject.RequestedEmail);

                //Retreive Info
                return ProfileObj.RetreiveProfileInfo(RequestObject.RequestingEmail, RequestObject.RequestedEmail, settingRetrieved);

            }
            else
            {
                return null;
            }
        }

        [HttpGet("{email}")]
        [Route("GetName")]  //api/GetProfileInfo/GetName/"email"
        public String GetName(String email)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            String name = UserInfoDataSet.Tables[0].Rows[0][0].ToString();
            return name;
        }

        [HttpGet("{email}")]
        [Route("GetPreferences")]  //api/GetProfileInfo/GetName/"email"
        public String GetPreferences(String email)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetPreferences";
            objCommand.Parameters.AddWithValue("@theUserEmail", email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            //deserialize settings object here or pass back and deserialize when you get it in preferences
            String userPreferences = UserInfoDataSet.Tables[0].Rows[0][0].ToString();
            //look at login to figure out deserialization biz
            return userPreferences;
        }
    }
}