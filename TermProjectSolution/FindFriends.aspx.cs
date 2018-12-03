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
                for(int i = 0; i < mySearchResults.Tables[0].Rows.Count; i++)
                {
                    
                }
                gvSearchResults.Visible = true;
            }
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            Button btnRequest = (Button)sender;
            GridViewRow gvr = (GridViewRow)btnRequest.NamingContainer;
            //from here send the info into the friend requests table
            //should be able to get the user email from the session object
            if (Request.Cookies["EmailCookie"] != null)
            {
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

                    Response.Write("Update returned: " + retVal);
                }
                
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feed.aspx");
        }
    }
}