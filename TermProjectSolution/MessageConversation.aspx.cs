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
    public partial class MessageConversation : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
            LoadMessages();
        }

        public void LoadMessages()
        {
            List<MessageBox> msgBoxes = new List<MessageBox>();
            foreach (Control msgBox in Form.Controls.OfType<MessageBox>())
            { 
                msgBoxes.Add((MessageBox)msgBox);
            }
            for(int i = 0; i < msgBoxes.Count; i++)
            {
                Form.Controls.Remove(msgBoxes[i]);
            }
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPGetMessages";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
            objCommand.Parameters.AddWithValue("@theFriendEmail", Session["friendEmail"].ToString());

            DataSet myMessages = objDB.GetDataSetUsingCmdObj(objCommand);

            if (myMessages.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < myMessages.Tables[0].Rows.Count; i++)
                {
                    MessageBox messageBox = (MessageBox)LoadControl("MessageBox.ascx");
                    //need to change how I am loading this
                    //maybe use a message object and pass it into the data bind method?
                    Message msg = new Message();
                    msg.UserEmail = myMessages.Tables[0].Rows[i][1].ToString();
                    msg.MessageBody = myMessages.Tables[0].Rows[i][3].ToString();
                    msg.MessageDate = DateTime.Parse(myMessages.Tables[0].Rows[i][4].ToString());
                    msg.MessageID = int.Parse(myMessages.Tables[0].Rows[i][0].ToString());
                    messageBox.DataBind(msg);

                    Form.Controls.Add(messageBox);
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            bool allGood = true;

            if (txtMessage.Text == "")
            {
                allGood = false;
                lblMessage.Text = "You must enter some text in the message box below.";
            }
            if (allGood)
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPInsertMessage";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
                objCommand.Parameters.AddWithValue("@theFriendEmail", Session["friendEmail"].ToString());
                objCommand.Parameters.AddWithValue("@theMessageBody", txtMessage.Text);
                objCommand.Parameters.AddWithValue("@theMessageDate", DateTime.Now);


                int retVal = objDB.DoUpdateUsingCmdObj(objCommand);
                if (retVal == 1)
                {
                    lblSendMessage.Text = "Message sent to " + Session["friendEmail"].ToString();
                    LoadMessages();

                    objCommand.CommandText = "TPCheckEmailNotifications";
                    objCommand.Parameters.Clear();

                    objCommand.Parameters.AddWithValue("@theUserEmail", Session["friendEmail"].ToString());
                    DataSet myEmail = objDB.GetDataSetUsingCmdObj(objCommand);
                    if (myEmail.Tables[0].Rows.Count > 0)
                    {
                        if (myEmail.Tables[0].Rows[0][1].ToString() == "True")
                        {
                            //send the email
                            Email friendEmail = new Email();
                            String senderAddress = Session["userEmail"].ToString();
                            String recipientAddress = Session["friendEmail"].ToString();
                            String subject = "New Message";
                            String message = senderAddress + " sent you a message. Log in to Fakebook to view it!";
                            friendEmail.SendMail(recipientAddress, senderAddress, subject, message);
                        }
                        else
                        {
                            Response.Write("You didn't send an email");
                        }
                    }

                }
                else
                {
                    lblSendMessage.Text = "There was a problem sending a message to " + Session["friendEmail"].ToString();
                }
            }
        }

        protected void btnGetMessages_Click(object sender, EventArgs e)
        {
            LoadMessages();
        }
    }
}