using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace TermProjectSolution
{
    public partial class FindFriends : System.Web.UI.Page
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

        public void InsertProfilePictures()
        {
            for (int i = 0; i < gvSearchResults.Rows.Count; i++)
            {
                Image myProfilePic = (Image)gvSearchResults.Rows[i].FindControl("imgProfilePic");
                myProfilePic.ImageUrl = "../Storage/" + objDB.GetField("profilePicUrl", i).ToString();
                if (myProfilePic.ImageUrl == "../Storage/")
                {
                    myProfilePic.ImageUrl = "../Storage/default-profile.png";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Need to use the API for this
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByName";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theName", txtSearch.Text);

            DataSet mySearchResults = objDB.GetDataSetUsingCmdObj(objCommand);

            if(mySearchResults.Tables[0].Rows.Count > 0)
            {
                gvSearchResults.DataSource = mySearchResults;
                gvSearchResults.DataBind();
                InsertProfilePictures();
                gvSearchResults.Visible = true;
            }
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            Button btnRequest = (Button)sender;
            GridViewRow gvr = (GridViewRow)btnRequest.NamingContainer;
            //from here send the info into the friend requests table
            //should be able to get the user email from the session object

            //HttpCookie myCookie = Request.Cookies["EmailCookie"];
            String userEmail = Session["userEmail"].ToString();
            String friendEmail = gvr.Cells[1].Text;
            if (userEmail.Equals(friendEmail))
            {
                Response.Write("You cannot request yourself as a friend!");
            }
            else
            {
                Response.Write("User email: " + userEmail + " Friend Email " + friendEmail);
                //insert Friend request record
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPInsertFriendRequest";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theUserEmail", userEmail);
                objCommand.Parameters.AddWithValue("@theFriendEmail", friendEmail);
                objCommand.Parameters.AddWithValue("@theRequestDate", System.DateTime.Now);

                int retVal = objDB.DoUpdateUsingCmdObj(objCommand);

                if(retVal > 0)
                {
                    objCommand.CommandText = "TPCheckEmailNotifications";
                    objCommand.Parameters.Clear();

                    objCommand.Parameters.AddWithValue("@theUserEmail", friendEmail);
                    DataSet myEmail = objDB.GetDataSetUsingCmdObj(objCommand);
                    if(myEmail.Tables[0].Rows.Count > 0)
                    {
                        if(myEmail.Tables[0].Rows[0][1].ToString() == "True")
                        {
                            //send the email
                            Email friendReqEmail = new Email();
                            String senderAddress = Session["userEmail"].ToString();
                            String recipientAddress = friendEmail;
                            String subject = "New Friend Request";
                            String message = senderAddress + " would like to be friends! Log into Fakebook to accept or deny the request.";
                            friendReqEmail.SendMail(recipientAddress, senderAddress, subject, message);
                        }
                        else 
                        {
                            Response.Write("You didn't send an email");
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feed.aspx");
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

        protected void btnStateSearch_Click(object sender, EventArgs e)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByLocation";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theState", txtState.Text);
            objCommand.Parameters.AddWithValue("@theCity", txtCity.Text);
            DataSet mySearchResults = objDB.GetDataSetUsingCmdObj(objCommand);

            if (mySearchResults.Tables[0].Rows.Count > 0)
            {
                gvSearchResults.DataSource = mySearchResults;
                gvSearchResults.DataBind();
                InsertProfilePictures();
                gvSearchResults.Visible = true;
            }
        }

        protected void btnOrganizationSearch_Click(object sender, EventArgs e)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPFindUsersByOrganization";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theOrganization", txtOrganization.Text);
            DataSet mySearchResults = objDB.GetDataSetUsingCmdObj(objCommand);

            if (mySearchResults.Tables[0].Rows.Count > 0)
            {
                gvSearchResults.DataSource = mySearchResults;
                gvSearchResults.DataBind();
                InsertProfilePictures();
                gvSearchResults.Visible = true;
            }
        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            advancedSearchContainer.Visible = true;
        }
    }
}