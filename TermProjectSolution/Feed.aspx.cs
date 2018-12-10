using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace TermProjectSolution
{
    public partial class Feed : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
            else
            {
                HttpCookie myCookie = new HttpCookie("myEmail");
                myCookie.Values["email"] = Session["userEmail"].ToString();
                myCookie.Expires = new DateTime(2020, 2, 1);
                Response.Cookies.Add(myCookie);
                LoadFeed(Session["userEmail"].ToString());
            }
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
            String email = "";
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
            else
            {
                email = Session["userEmail"].ToString();
            }

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
                    PostImage(email, timeStamp);
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
                    PostStatus(email);
                }
            }
        }//End of Post Click

        void PostImage(String email, String timeStamp)
        {
            if (FileImageUpload.HasFile)
            {
                string extension = System.IO.Path.GetExtension(FileImageUpload.FileName);

                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                {
                    FileImageUpload.PostedFile.SaveAs(Server.MapPath("~/Storage/") + email + "-Post" + timeStamp + ".png");
                    DBConnect objDB = new DBConnect();
                    SqlCommand objCommand = new SqlCommand();
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_InsertPost";
                    objCommand.Parameters.AddWithValue("@Email", email);
                    objCommand.Parameters.AddWithValue("@Body", ImageCaptionTextBox.Text);
                    objCommand.Parameters.AddWithValue("@ImageURL", email + "-Post" + timeStamp + ".png");
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
        }//End of Post Image

        void PostStatus(String email)
        {
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

        void LoadFeed(String userEmail) {


            FindFriendsClass ffObject = new FindFriendsClass();
            ffObject.userEmail = userEmail;
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

                FindFriendsClass[] FriensdListArray = js.Deserialize<FindFriendsClass[]>(data);
                 
                //Retrieve Own Feed
               


                if (FriensdListArray.Length == 0) {
                    //No Friends Found
                }
                if (FriensdListArray.Length > 0) {
                    for (int i = 0; i < FriensdListArray.Length; i++) {
                      
                        DBConnect objDB = new DBConnect();
                        SqlCommand objCommand = new SqlCommand();
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_LoadRegularFeed";
                        objCommand.Parameters.AddWithValue("@Email", FriensdListArray[i].userEmail);
                        DataSet FeedDS = objDB.GetDataSetUsingCmdObj(objCommand);
                        if (FeedDS.Tables[0].Rows.Count > 0)
                        {
                            for (int n = 0; n < FeedDS.Tables[0].Rows.Count; n++)
                            {
                                ProfileFeed feed = (ProfileFeed)LoadControl("ProfileFeed.ascx");
                                Posts postObject = new Posts();
                                postObject.PostID = FeedDS.Tables[0].Rows[n][0].ToString();
                                postObject.UserEmail = FeedDS.Tables[0].Rows[n][1].ToString();
                                postObject.PostBody = FeedDS.Tables[0].Rows[n][2].ToString();
                                postObject.DatePosted = DateTime.Parse(FeedDS.Tables[0].Rows[n][4].ToString());
                                postObject.ImageURL = FeedDS.Tables[0].Rows[n][3].ToString();
                                feed.DataBind(postObject);
                                form1.Controls.Add(feed);
                            }
                        }
                    }
                }

                //Retrieve Users Own Feed

                DBConnect dbConnection = new DBConnect();
                SqlCommand objCommand1 = new SqlCommand();
                objCommand1.CommandType = CommandType.StoredProcedure;
                objCommand1.CommandText = "TP_LoadProfileFeed";
                objCommand1.Parameters.AddWithValue("@Email", userEmail);
                DataSet UserFeedDS = dbConnection.GetDataSetUsingCmdObj(objCommand1);

                if (UserFeedDS.Tables[0].Rows.Count == 0) {
                    //No Feed Available
                }
                if (UserFeedDS.Tables[0].Rows.Count > 0)
                {
                    for (int n = 0; n < UserFeedDS.Tables[0].Rows.Count; n++)
                    {
                        ProfileFeed feed1 = (ProfileFeed)LoadControl("ProfileFeed.ascx");
                        Posts postObject = new Posts();
                        postObject.PostID = UserFeedDS.Tables[0].Rows[n][0].ToString();
                        postObject.UserEmail = UserFeedDS.Tables[0].Rows[n][1].ToString();
                        postObject.PostBody = UserFeedDS.Tables[0].Rows[n][2].ToString();
                        postObject.DatePosted = DateTime.Parse(UserFeedDS.Tables[0].Rows[n][4].ToString());
                        postObject.ImageURL = UserFeedDS.Tables[0].Rows[n][3].ToString();
                        feed1.DataBind(postObject);
                        form1.Controls.Add(feed1);
                    }
                }

            }
            catch (Exception errorEx)
            {
                Response.Write(errorEx.Message);
            }
        }


    }
}