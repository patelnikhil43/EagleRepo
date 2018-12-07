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
    public partial class Profile : System.Web.UI.Page
    {
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        protected void Page_Load(object sender, EventArgs e)
        {
            //UserEmail Cookie
            //HttpCookie tempCookie = new HttpCookie("UserEmailCookie");
            //tempCookie.Values["Email"] = email;
            //tempCookie.Expires = new DateTime(2020, 2, 1);
            //Response.Cookies.Add(tempCookie);


            //Set User Profile Name
            SetUserProfileName();
            //Set User Profile Picture
            SetUserProfilePicture();
            //Set User Profile Information
            SetUserProfileInformation();
            //Set Friend List
            SetFriendList();

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPUpdateStatusLogout";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());

            objDB.DoUpdateUsingCmdObj(objCommand);
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        void SetUserProfileName()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";

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
            String email = plainTextEmail;
            //End of decoder

            objCommand.Parameters.AddWithValue("@email", email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            UserNameLabel.Text = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
        }

        void SetUserProfilePicture()
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
            String email = plainTextEmail;
            //End of decoder

            //Get profile image link if exist

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserProfileURL";
            objCommand.Parameters.AddWithValue("@Email", email);

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
            String email = plainTextEmail;
            //End of decoder

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", email);

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
            String email = plainTextEmail;
            //End of decoder
            if (ProfileImageUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(ProfileImageUpload.FileName);

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    ProfileImageUpload.PostedFile.SaveAs(Server.MapPath("~/Storage/") + email + "-ProfileImage.png");
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TPUpdateProfileURL";
                    objCommand.Parameters.AddWithValue("@email", email);
                    objCommand.Parameters.AddWithValue("@URL", email + "-ProfileImage.png");
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Write("Only .jpg, .png, or .jpeg allowed");
                }
            }
        }

       

        void SetFriendList()
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
            String email = plainTextEmail;
            //End of decoder


            FindFriendsClass ffObject = new FindFriendsClass();
            ffObject.userEmail = email;
            JavaScriptSerializer js = new JavaScriptSerializer();  //Coverts Object into JSON String
            String jsonffObject = js.Serialize(ffObject);
            try
            {
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create("http://localhost:55065/api/FindFriends/FindFriendsDS/");
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

                FindFriendsClass[] CreditInfoData = js.Deserialize<FindFriendsClass[]>(data);
                FriendListGV.DataSource = CreditInfoData;
                FriendListGV.DataBind();




            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        }

        protected void FriendListGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = FriendListGV.Rows[index];
           
            var email = (row.FindControl("friendEmailID") as Label).Text; 
            //View Profile Cookie          
            HttpCookie tempCookie = new HttpCookie("ViewProfile");
            tempCookie.Values["Email"] = email;
            tempCookie.Expires = new DateTime(2020, 2, 1);
            Response.Cookies.Add(tempCookie);
            Response.Redirect("OtherUserProfile.aspx");
        }
    }
}