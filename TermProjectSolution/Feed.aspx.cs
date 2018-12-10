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
    }
}