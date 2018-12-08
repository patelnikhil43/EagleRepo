using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using System.Data;
using System.Data.SqlClient;

namespace TermProjectSolution
{
    public partial class Messages : System.Web.UI.Page
    {
        //need to figure out how to get friends that are online
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            //getfriends and load into gridview
            getFriendsOnline();
            //if (gvFriendsOnline.Rows.Count > 0)
            //{
            //    //txtEmail.Text = gvFriendsOnline.Rows[0].Cells[1].Text;
            //    LoadMessages();
            //}
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

        //public void LoadMessages()
        //{
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.CommandText = "TPGetMessages";
        //    objCommand.Parameters.Clear();

        //    objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
        //    objCommand.Parameters.AddWithValue("@theFriendEmail", txtEmail.Text);

        //    DataSet myMessages = objDB.GetDataSetUsingCmdObj(objCommand);

        //    if (myMessages.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < myMessages.Tables[0].Rows.Count; i++)
        //        {
        //            MessageBox messageBox = (MessageBox)LoadControl("MessageBox.ascx");
        //            //need to change how I am loading this
        //            //maybe use a message object and pass it into the data bind method?
        //            Message msg = new Message();
        //            msg.UserEmail = myMessages.Tables[0].Rows[i][1].ToString();
        //            msg.MessageBody = myMessages.Tables[0].Rows[i][3].ToString();
        //            msg.MessageDate = DateTime.Parse(myMessages.Tables[0].Rows[i][4].ToString());
        //            msg.MessageID = int.Parse(myMessages.Tables[0].Rows[i][0].ToString());
        //            messageBox.DataBind(msg);

        //            Form.Controls.Add(messageBox);
        //        }
        //    }
        //}

        protected void getFriendsOnline()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetFriendsOnline";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());

            DataSet myFriendsOnline = objDB.GetDataSetUsingCmdObj(objCommand);

            gvFriendsOnline.DataSource = myFriendsOnline;
            gvFriendsOnline.DataBind();
            gvFriendsOnline.Visible = true;
            if(gvFriendsOnline.Rows.Count == 0)
            {
                lblNoMessages.Text = "You have no friends currently online.";
            }
            else
            {
                for(int i = 0; i < gvFriendsOnline.Rows.Count; i++)
                {
                    Image myProfilePic = (Image)gvFriendsOnline.Rows[i].FindControl("imgProfilePic");
                    myProfilePic.ImageUrl = objDB.GetField("profilePicUrl", i).ToString();
                    if(myProfilePic.ImageUrl == "")
                    {
                        myProfilePic.ImageUrl = "../Storage/default-profile.png";
                    }
                }
            }
        }

        protected void btnGetFriendsOnline_Click(object sender, EventArgs e)
        {
            getFriendsOnline();
        }

        //protected void btnSend_Click(object sender, EventArgs e)
        //{
        //    bool allGood = true;
        //    if(txtEmail.Text == "")
        //    {
        //        allGood = false;
        //        lblEmail.Text = "You must enter an email address.";
        //    }
        //    else
        //    {
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.CommandText = "TPCheckIfUserExist";
        //        objCommand.Parameters.Clear();

        //        objCommand.Parameters.AddWithValue("@Email", txtEmail.Text);

        //        DataSet myUser = objDB.GetDataSetUsingCmdObj(objCommand);

        //        if(myUser.Tables[0].Rows.Count <= 0)
        //        {
        //            allGood = false;
        //            lblEmail.Text = "Email address not found.";
        //        }
        //    }
        //    if(txtMessage.Text == "")
        //    {
        //        allGood = false;
        //        lblMessage.Text = "You must enter some text in the message box below.";
        //    }
        //    if (allGood)
        //    {
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.CommandText = "TPInsertMessage";
        //        objCommand.Parameters.Clear();

        //        objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
        //        objCommand.Parameters.AddWithValue("@theFriendEmail", txtEmail.Text);
        //        objCommand.Parameters.AddWithValue("@theMessageBody", txtMessage.Text);
        //        objCommand.Parameters.AddWithValue("@theMessageDate", DateTime.Now);

        //        Session.Add("friendEmail", txtEmail.Text);

        //        int retVal = objDB.DoUpdateUsingCmdObj(objCommand);
        //        if (retVal == 1)
        //        {
        //            lblSendMessage.Text = "Message sent to " + txtEmail.Text;
        //            LoadMessages();
        //        }
        //        else
        //        {
        //            lblSendMessage.Text = "There was a problem sending a message to " + txtEmail.Text;
        //        }
        //    }
        //}

        //protected void btnGetMessages_Click(object sender, EventArgs e)
        //{
        //    Session.Add("friendEmail", txtEmail.Text);
        //    LoadMessages();
        //}

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Button btnSendMessage = (Button)sender;
            GridViewRow gvr = (GridViewRow)btnSendMessage.NamingContainer;

            String friendEmail = gvr.Cells[1].Text;
            //txtEmail.Text = friendEmail;
            //LoadMessages();

            Session.Add("friendEmail", friendEmail);
            Response.Redirect("MessageConversation.aspx");
        }
    }
}