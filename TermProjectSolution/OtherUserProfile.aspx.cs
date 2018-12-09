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
    public partial class OtherUserProfile : System.Web.UI.Page
    {
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
            if (!IsPostBack) {

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

              
                SetUserProfileName();
                SetUserProfilePicture();
                SetUserProfileInformation(RequestingUserEmail);
                SetFriendList();
                SetImageGallery(RequestingUserEmail);
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

        void SetUserProfileInformation(String RequestingUserEmail)
        {
           
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

        void SetFriendList() {
            var Email = Request.Cookies["ViewProfile"]["Email"].ToString();

            FindFriendsClass ffObject = new FindFriendsClass();
            ffObject.userEmail = Email;
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

                FindFriendsClass[] FriendInfoData = js.Deserialize<FindFriendsClass[]>(data);
                if (FriendInfoData.Length == 0)
                {
                    NoFriendsLabel.Visible = true;
                }
                else
                {
                    FriendListGV.DataSource = FriendInfoData;
                    FriendListGV.DataBind();
                }



            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        }

        void SetImageGallery(String RequestingUserEmail) {

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
                WebRequest request = WebRequest.Create("http://localhost:55065/api/ImageGallery/GetImages/");
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

                Utilities.GalleryImagesClass[] ImagesInfo = js.Deserialize<Utilities.GalleryImagesClass[]>(data);

                if (ImagesInfo.Length == 0)
                {
                    //Profile Info Not Available
                    NoImagesLabel.Visible = true;
                }
                else
                {
                    ImageGalleryGV.DataSource = ImagesInfo;
                    ImageGalleryGV.DataBind();
                    for (int i = 0; i < ImageGalleryGV.Rows.Count; i++)
                    {
                     Image PhotoCollectionImage = (Image)ImageGalleryGV.Rows[i].FindControl("GalleryCollectionImages");
                     var tempURL = "../Storage/" + (ImageGalleryGV.Rows[i].FindControl("GalleryImageURL") as Label).Text;
                        PhotoCollectionImage.ImageUrl = tempURL;
                    }

                }

            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }

        }

        protected void FriendListGV_RowCommand(object sender, GridViewCommandEventArgs e)
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

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = FriendListGV.Rows[index];

            var email = (row.FindControl("friendEmailID") as Label).Text;
            //View Profile Cookie          
            HttpCookie tempCookie = new HttpCookie("ViewProfile");
            tempCookie.Values["Email"] = email;
            tempCookie.Expires = new DateTime(2020, 2, 1);
            Response.Cookies.Add(tempCookie);
            if (email == RequestingUserEmail)
            {
                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Redirect("OtherUserProfile.aspx");
            }
        }

        protected void ChoosePostTypeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChoosePostTypeDD.SelectedValue == "PhotoPost")
            {
                TypeImagePostDiv.Visible = true;
                TypeStatusPostDiv.Visible = false;
            }
            if (ChoosePostTypeDD.SelectedValue == "StatusPost")
            {
                TypeStatusPostDiv.Visible = true;
                TypeImagePostDiv.Visible = false;
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
            String VisitingUser = plainTextEmail;
            //End of Decoder

            ArrayList ImagePostErrorArray = new ArrayList();
            //Check Which Options is selected
            if (ChoosePostTypeDD.SelectedValue == "PhotoPost")
            {
                //Validate
                if (!FileImageUpload.HasFile)
                {
                    ImagePostErrorArray.Add("Select an Image");
                }
                string extension = System.IO.Path.GetExtension(FileImageUpload.FileName);
                if (extension.ToLower() != ".jpg" && extension.ToLower() != ".png" && extension.ToLower() != ".jpeg")
                {
                    ImagePostErrorArray.Add("ONLY .JPG, .PNG OR .JPEG Images are allowed");
                }
                if (ImageCaptionTextBox.Text == "")
                {
                    ImagePostErrorArray.Add("Enter Caption");
                }
                if (ImagePostErrorArray.Count == 0)
                {
                    var timeStamp = DateTime.Now.ToString();
                    timeStamp = timeStamp.Replace(" ", "");
                    timeStamp = timeStamp.Replace(":", "");
                    timeStamp = timeStamp.Replace("/", "");

                    //Post Image
                    PostImage(VisitingUser, timeStamp);
                }
            }

            ArrayList StatusPostErrorArray = new ArrayList();
            if (ChoosePostTypeDD.SelectedValue == "StatusPost")
            {
                if (StatusPostCaptionTextBox.Text == "")
                {
                    StatusPostErrorArray.Add("Enter Valid Caption");
                }
                if (StatusPostErrorArray.Count == 0)
                {
                    PostStatus(VisitingUser);
                }
            }
        }//End of Post

        void PostImage(String VisitingUser, String timeStamp)
        {
            var VisitedUser = Request.Cookies["ViewProfile"]["Email"].ToString();

            if (FileImageUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(FileImageUpload.FileName);

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    FileImageUpload.PostedFile.SaveAs(Server.MapPath("~/Storage/") + VisitedUser + "-Post" + timeStamp + ".png");
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_InsertPost";
                    objCommand.Parameters.AddWithValue("@Email", VisitingUser);
                    objCommand.Parameters.AddWithValue("@Body", ImageCaptionTextBox.Text);
                    objCommand.Parameters.AddWithValue("@ImageURL", VisitedUser + "-Post" + timeStamp + ".png");
                    objCommand.Parameters.AddWithValue("@DatePosted", DateTime.Now);
                    objCommand.Parameters.AddWithValue("@Tag", "YES");
                    objCommand.Parameters.AddWithValue("@PostType", "IMAGEPOST");
                    objCommand.Parameters.AddWithValue("@postingToUser", VisitedUser);
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Write("Only .jpg, .png, or .jpeg allowed");
                }
            }
        }

        void PostStatus(String VisitingUser)
        {
            var VisitedUser = Request.Cookies["ViewProfile"]["Email"].ToString();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertStatusPost";
            objCommand.Parameters.AddWithValue("@userEmail", VisitingUser);
            objCommand.Parameters.AddWithValue("@postBody", StatusPostCaptionTextBox.Text);
            objCommand.Parameters.AddWithValue("@datePosted", DateTime.Now);
            objCommand.Parameters.AddWithValue("@tag", "NO");
            objCommand.Parameters.AddWithValue("@postType", "STATUSPOST");
            objCommand.Parameters.AddWithValue("@postingToUser", VisitedUser);

            objDB.DoUpdateUsingCmdObj(objCommand);
            Response.Redirect(Request.Url.AbsoluteUri);
        }


    }
}