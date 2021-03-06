﻿using System;
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
    public partial class FriendRequests : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
            if (!IsPostBack)
            {
                getFriendRequests();
            }
        }

        public void getFriendRequests()
        {
            //need to load in all friend requests that have not been accepted or rejected
            String userEmail = Session["userEmail"].ToString();
            
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetFriendRequests";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theEmail", userEmail);

            DataSet myFriendRequests = objDB.GetDataSetUsingCmdObj(objCommand);

            if (myFriendRequests.Tables[0].Rows.Count > 0)
            {
                
                //for loop gets rid of friend requests that have been accepted or rejected
                for (int i = 0; i < myFriendRequests.Tables[0].Rows.Count; i++)
                {
                    if (myFriendRequests.Tables[0].Rows[i][4].ToString() == "True" || myFriendRequests.Tables[0].Rows[i][4].ToString() == "False")
                    {
                        myFriendRequests.Tables[0].Rows.RemoveAt(i);
                        i--;
                    }
                }
            }

            gvFriendRequests.DataSource = myFriendRequests;
            gvFriendRequests.DataBind();
        }

        protected void btnAcceptRequest_Click(object sender, EventArgs e)
        {
            //need to update isAccepted column in friendrequest table
            //need to add two records: one for friendship one way and one for it the other way
            Button btnAccept = (Button)sender;
            GridViewRow gvr = (GridViewRow)btnAccept.NamingContainer;

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPInsertFriend";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
            objCommand.Parameters.AddWithValue("@theFriendEmail", gvr.Cells[0].Text);
            DateTime friendDate = System.DateTime.Now;
            objCommand.Parameters.AddWithValue("@theFriendDate", friendDate);

            int retVal1 = objDB.DoUpdateUsingCmdObj(objCommand);

            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theUserEmail", gvr.Cells[0].Text);
            objCommand.Parameters.AddWithValue("@theFriendEmail", Session["userEmail"].ToString());
            objCommand.Parameters.AddWithValue("@theFriendDate", friendDate);

            int retVal2 = objDB.DoUpdateUsingCmdObj(objCommand);

            //get rid of friendrequest in the table now
            if (retVal1 == 1 && retVal2 == 1)
            {
                lblMessage.Text = "You accepted " + gvFriendRequests.Rows[0].Cells[0].Text + " as a friend!";
                objCommand.Parameters.Clear();
                objCommand.CommandText = "TPUpdateFriendRequest";
                objCommand.Parameters.AddWithValue("@theIsAccepted", 1);
                objCommand.Parameters.AddWithValue("@theUserEmail", gvr.Cells[0].Text);
                objCommand.Parameters.AddWithValue("@theFriendEmail", Session["userEmail"].ToString());

                objDB.DoUpdateUsingCmdObj(objCommand);
                

                objCommand.CommandText = "TPCheckEmailNotifications";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theUserEmail", gvFriendRequests.Rows[0].Cells[0].Text);
                DataSet myEmail = objDB.GetDataSetUsingCmdObj(objCommand);
                if (myEmail.Tables[0].Rows.Count > 0)
                {
                    if (myEmail.Tables[0].Rows[0][1].ToString() == "True")
                    {
                        //send the email
                        Email friendEmail = new Email();
                        String senderAddress = Session["userEmail"].ToString();
                        String recipientAddress = gvFriendRequests.Rows[0].Cells[0].Text;
                        String subject = "Friend Request Accepted";
                        String message = senderAddress + " accepted your friend request!";
                        friendEmail.SendMail(recipientAddress, senderAddress, subject, message);
                    }
                    else
                    {
                        Response.Write("You didn't send an email");
                    }
                }

                getFriendRequests();
            }
            else
            {

            }
        }

        protected void btnRejectRequest_Click(object sender, EventArgs e)
        {
            //need to update isAccepted column in friendrequest table
            Button btnReject = (Button)sender;
            GridViewRow gvr = (GridViewRow)btnReject.NamingContainer;
            
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPUpdateFriendRequest";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theIsAccepted", 0);
            objCommand.Parameters.AddWithValue("@theUserEmail", gvr.Cells[0].Text);
            objCommand.Parameters.AddWithValue("@theFriendEmail", Session["userEmail"].ToString());

            objDB.DoUpdateUsingCmdObj(objCommand);
            getFriendRequests();

            objCommand.CommandText = "TPCheckEmailNotifications";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theUserEmail", gvFriendRequests.Rows[0].Cells[0].Text);
            DataSet myEmail = objDB.GetDataSetUsingCmdObj(objCommand);
            if (myEmail.Tables[0].Rows.Count > 0)
            {
                if (myEmail.Tables[0].Rows[0][1].ToString() == "True")
                {
                    //send the email
                    Email friendEmail = new Email();
                    String senderAddress = Session["userEmail"].ToString();
                    String recipientAddress = gvFriendRequests.Rows[0].Cells[0].Text;
                    String subject = "Friend Request Rejected";
                    String message = senderAddress + " rejected your friend request. :(";
                    friendEmail.SendMail(recipientAddress, senderAddress, subject, message);
                }
                else
                {
                    Response.Write("You didn't send an email");
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            getFriendRequests();
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
    }
}