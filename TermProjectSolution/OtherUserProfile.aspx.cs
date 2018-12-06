using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace TermProjectSolution
{
    public partial class OtherUserProfile : System.Web.UI.Page
    {
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                SetUserProfileName();
                SetUserProfilePicture();
                SetUserProfileInformation();
            }
        }

        void SetUserProfileName() {
           var Email = Request.Cookies["ViewProfile"]["Email"].ToString();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", Email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            UserNameLabel.Text = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
        }

        void SetUserProfilePicture()
        {
            var Email = Request.Cookies["ViewProfile"]["Email"].ToString();
            //Get profile image link if exist

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserProfileURL";
            objCommand.Parameters.AddWithValue("@Email", Email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            String url = UserInfoDataSet.Tables[0].Rows[0]["profilePicURL"].ToString();
            if (url == "")
            {
                UserProfileImage.ImageUrl = "../Storage/default-profile.png";
            }
            else
            {
                UserProfileImage.ImageUrl = "../Storage/" + url;
            }

        }

        void SetUserProfileInformation()
        {
            //Decoder
            HttpCookie myCookie = Request.Cookies["LoginCookie"];
            //txtEmail.Text = myCookie.Values["Email"];
            //txtPassword.Text = myCookie.Values["Password"];
            String encryptedEmail = myCookie.Values["Email"];

            Byte[] encryptedEmailBytes = Convert.FromBase64String(encryptedEmail);
            Byte[] emailBytes;
            String plainTextEmail;

            UTF8Encoding encoder = new UTF8Encoding();

            RijndaelManaged rmEncryption = new RijndaelManaged();
            MemoryStream memStream = new MemoryStream();
            CryptoStream decryptionStream = new CryptoStream(memStream, rmEncryption.CreateDecryptor(key, vector), CryptoStreamMode.Write);

            //Email
            decryptionStream.Write(encryptedEmailBytes, 0, encryptedEmailBytes.Length);
            decryptionStream.FlushFinalBlock();

            memStream.Position = 0;
            emailBytes = new Byte[memStream.Length];
            memStream.Read(emailBytes, 0, emailBytes.Length);

            decryptionStream.Close();
            memStream.Close();

            plainTextEmail = encoder.GetString(emailBytes);
            String RequestingUserEmail = plainTextEmail;
            //End of decoder
            var RequestedUserEmail = Request.Cookies["ViewProfile"]["Email"].ToString();

            ProfileRequest ProfileObject = new ProfileRequest();
            ProfileObject.Token = "1234";
            ProfileObject.RequestedEmail = RequestedUserEmail;
            ProfileObject.RequestingEmail = RequestingUserEmail;

            JavaScriptSerializer js = new JavaScriptSerializer();  //Coverts Object into JSON String
            String jsonffObject = js.Serialize(ProfileObject);

            try
            {
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create("http://localhost:55065/api/GetProfileInfo/GetProfileInfoMethod/");
                request.Method = "POST";
                request.ContentLength = jsonffObject.Length;
                request.ContentType = "application/json";

                // Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonffObject);
                writer.Flush();
                writer.Close();

                // Read the data from the Web Response, which requires working with streams.

                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();

                reader.Close();
                response.Close();

                Utilities.Profile[] ProfileInfo = js.Deserialize<Utilities.Profile[]>(data);

                if (ProfileInfo.Length == 0)
                {
                    //Profile Info Not Available
                }
                else {

                    for (int i = 0; i < ProfileInfo.Length; i++) {

                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        cell1.Text = "Name: " + ProfileInfo[i].name;
                        cell1.Style.Add("padding", "10px");
                        TableCell cell2 = new TableCell();
                        cell2.Text = "Address: " + ProfileInfo[i].address;
                        cell2.Style.Add("padding", "10px");
                        row.Cells.Add(cell1);
                        row.Cells.Add(cell2);
                        UserProfileTable.Rows.Add(row);

                        TableRow row2 = new TableRow();
                        TableCell cell11 = new TableCell();
                        cell11.Text = "City: " + ProfileInfo[i].city;
                        cell11.Style.Add("padding", "10px");
                        TableCell cell12 = new TableCell();
                        cell12.Text = "Zip: " + ProfileInfo[i].zip;
                        cell12.Style.Add("padding", "10px");
                        row2.Cells.Add(cell11);
                        row2.Cells.Add(cell12);
                        UserProfileTable.Rows.Add(row2);

                    }

                }




            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        

    }

        protected void ChangeUserProfileImageButton_Click(object sender, EventArgs e)
        {

        }

        protected void FriendListGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}