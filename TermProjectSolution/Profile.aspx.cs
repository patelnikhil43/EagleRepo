using System;
using System.Collections;
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

            //Set User Profile Name
            //SetUserProfileName(email);
            //Set User Profile Picture
            SetUserProfilePicture(email);
            //Set User Profile Information
            SetUserProfileInformation(email);
            //Set Friend List
            SetFriendList(email);

            LoadFeed(email);

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPUpdateStatusLogout";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());

            dbConnection.DoUpdateUsingCmdObj(objCommand);
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        void SetUserProfileName(String email)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetUserInfo";
            objCommand.Parameters.AddWithValue("@email", email);

            DataSet UserInfoDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            UserNameLabel.Text = UserInfoDataSet.Tables[0].Rows[0]["name"].ToString();
        }

        void SetUserProfilePicture(String email)
        {
            

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

        void SetUserProfileInformation(String email)
        {
           

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

       

        void SetFriendList(String email)
        {
           


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

        protected void ChoosePostTypeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChoosePostTypeDD.SelectedValue == "PhotoPost") {
                TypeImagePostDiv.Visible = true;
                TypeStatusPostDiv.Visible = false;
            }
            if (ChoosePostTypeDD.SelectedValue == "StatusPost") {
                TypeStatusPostDiv.Visible = true;
                TypeImagePostDiv.Visible = false;
            }
        }

        protected void TagFriendsDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TagFriendsDD.SelectedValue == "YES") {
                SetTagFriendsList();
            } 
               
               
        }

        void SetTagFriendsList() {
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
                TagFriendsGV.DataSource = CreditInfoData;
                TagFriendsGV.DataBind();




            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        }

        protected void PostButton_Click(object sender, EventArgs e)
        {
            //Get User Email
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
            //End of Decoder

            ArrayList ImagePostErrorArray = new ArrayList();
            //Check Which Options is selected
            if (ChoosePostTypeDD.SelectedValue == "PhotoPost") {
                //Validate
                if (!FileImageUpload.HasFile) {
                    ImagePostErrorArray.Add("Select an Image");
                }
                string extension = System.IO.Path.GetExtension(FileImageUpload.FileName);
                if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png" && extension.ToLower() != ".jpeg") {
                    ImagePostErrorArray.Add("ONLY .JPG, .PNG OR .JPEG Images are allowed");
                }
                if (ImageCaptionTextBox.Text == "") {
                    ImagePostErrorArray.Add("Enter Caption");
                }
                if (ImagePostErrorArray.Count == 0) {
                    var timeStamp = DateTime.Now.ToString();
                    timeStamp = timeStamp.Replace(" ", "");
                    timeStamp = timeStamp.Replace(":", "");
                    timeStamp = timeStamp.Replace("/", "");

                    //Post Image
                    PostImage(email, timeStamp);
                }
            }

            ArrayList StatusPostErrorArray = new ArrayList();
            if (ChoosePostTypeDD.SelectedValue == "StatusPost") {
                if (StatusPostCaptionTextBox.Text == "") {
                    StatusPostErrorArray.Add("Enter Valid Caption");
                }
                if (StatusPostErrorArray.Count == 0) {
                    PostStatus(email);
                }
            }

            

           

        }
        void PostImage(String email, String timeStamp) {
            if (FileImageUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(FileImageUpload.FileName);

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    FileImageUpload.PostedFile.SaveAs(Server.MapPath("~/Storage/") + email + "-Post"+ timeStamp + ".png");
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_InsertPost";
                    objCommand.Parameters.AddWithValue("@Email", email); 
                    objCommand.Parameters.AddWithValue("@Body", ImageCaptionTextBox.Text);
                    objCommand.Parameters.AddWithValue("@ImageURL", email + "-Post" + timeStamp+ ".png");
                    objCommand.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    objCommand.Parameters.AddWithValue("@Tag", "YES");
                    objCommand.Parameters.AddWithValue("@PostType", "IMAGEPOST"); 
                    objCommand.Parameters.AddWithValue("@postingToUser", email);
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Write("Only .jpg, .png, or .jpeg allowed");
                }
            }
        }

        void PostStatus(String email) {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertStatusPost";
            objCommand.Parameters.AddWithValue("@userEmail", email);
            objCommand.Parameters.AddWithValue("@postBody", StatusPostCaptionTextBox.Text);
            objCommand.Parameters.AddWithValue("@datePosted", DateTime.Now);
            objCommand.Parameters.AddWithValue("@tag", "NO");
            objCommand.Parameters.AddWithValue("@postType", "STATUSPOST");
            objCommand.Parameters.AddWithValue("@postingToUser", email);

            objDB.DoUpdateUsingCmdObj(objCommand);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void StatusPostTagDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StatusPostTagDD.SelectedValue == "YES")
            {
                SetStatusTagFriendsList();
            }
        }
        void SetStatusTagFriendsList() {
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
                StatusPostTagGV.DataSource = CreditInfoData;
                StatusPostTagGV.DataBind();




            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        }

        void LoadFeed(String email)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_LoadProfileFeed";
            objCommand.Parameters.AddWithValue("@Email", email);
           DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);

            if (FeedDS.Tables[0].Rows.Count > 0) {
                for (int i = 0; i < FeedDS.Tables[0].Rows.Count; i++)
                {
                    ProfileFeed feed = (ProfileFeed)LoadControl("ProfileFeed.ascx");
                    Posts postObject = new Posts();
                    postObject.PostID = FeedDS.Tables[0].Rows[i][0].ToString();
                    postObject.UserEmail = FeedDS.Tables[0].Rows[i][1].ToString();
                    postObject.PostBody = FeedDS.Tables[0].Rows[i][2].ToString();
                    postObject.DatePosted =DateTime.Parse(FeedDS.Tables[0].Rows[i][4].ToString());
                    postObject.ImageURL = FeedDS.Tables[0].Rows[i][3].ToString();
                    feed.DataBind(postObject);
                    form1.Controls.Add(feed);
                }
            }

        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateProfile.aspx");
        }
    }
}