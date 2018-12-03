using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace TermProjectSolution
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Set User Profile Name
            SetUserProfileName();
            //Set User Profile Picture
            SetUserProfilePicture();
            //Set User Profile Information
            SetUserProfileInformation();
            //Set Friend List
            //SetFriendList();

        }
        void SetUserProfileName()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", Request.Cookies["EmailCookie"]["Email"]);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            UserNameLabel.Text = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
        }

        void SetUserProfilePicture()
        {
            UserProfileImage.ImageUrl = "../Storage/" + Request.Cookies["EmailCookie"]["Email"] + "-ProfileImage.png";
        }

        void SetUserProfileInformation()
        {

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", Request.Cookies["EmailCookie"]["Email"]);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            UserNameLabel.Text = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();


            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "Name: " + UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
            cell1.Style.Add("padding", "10px");
            TableCell cell2 = new TableCell();
            cell2.Text = "Address: " + UserInfoDataSet.Tables[0].Rows[0]["address"].ToString();
            cell2.Style.Add("padding", "10px");
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            UserProfileTable.Rows.Add(row);

            TableRow row2 = new TableRow();
            TableCell cell11 = new TableCell();
            cell11.Text = "City: " + UserInfoDataSet.Tables[0].Rows[0]["city"].ToString();
            cell11.Style.Add("padding", "10px");
            TableCell cell12 = new TableCell();
            cell12.Text = "Zip: " + UserInfoDataSet.Tables[0].Rows[0]["zip"].ToString();
            cell12.Style.Add("padding", "10px");
            row2.Cells.Add(cell11);
            row2.Cells.Add(cell12);
            UserProfileTable.Rows.Add(row2);




        }

        protected void ChangeUserProfileImageButton_Click(object sender, EventArgs e)
        {
            if (ProfileImageUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(ProfileImageUpload.FileName);

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    ProfileImageUpload.PostedFile.SaveAs(Server.MapPath("~/Storage/") + Request.Cookies["EmailCookie"]["Email"] + "-ProfileImage.png");
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TPUpdateProfileURL";
                    objCommand.Parameters.AddWithValue("@email", Request.Cookies["EmailCookie"]["Email"]);
                    objCommand.Parameters.AddWithValue("@URL", Request.Cookies["EmailCookie"]["Email"] + "-ProfileImage.png");
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Write("Only .jpg, .png, or .jpeg allowed");
                }
            }
        }

        //void SetFriendList()
        //{
        //    FindFriendsClass ffObject = new FindFriendsClass();
        //    ffObject.userEmail = Request.Cookies["EmailCookie"]["Email"];
        //    JavaScriptSerializer js = new JavaScriptSerializer();  //Coverts Object into JSON String
        //    String jsonffObject = js.Serialize(ffObject);
        //    try
        //    {
        //        // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
        //        WebRequest request = WebRequest.Create("http://localhost:44395/api/FindFriends/FindFriendsDS/");
        //        request.Method = "POST";
        //        request.ContentLength = jsonffObject.Length;
        //        request.ContentType = "application/json";

        //        // Write the JSON data to the Web Request
        //        StreamWriter writer = new StreamWriter(request.GetRequestStream());
        //        writer.Write(jsonffObject);
        //        writer.Flush();
        //        writer.Close();

        //        // Read the data from the Web Response, which requires working with streams.

        //        WebResponse response = request.GetResponse();
        //        Stream theDataStream = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(theDataStream);
        //        String data = reader.ReadToEnd();
        //        reader.Close();
        //        response.Close();

        //        DataSet FriendListDS = js.Deserialize<DataSet>(data);


        //    }
        //    catch (Exception errorEx)
        //    {
        //        Response.Write(errorEx.Message);
        //    }
        //}
    }
}