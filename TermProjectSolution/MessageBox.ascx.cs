using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace TermProjectSolution
{
    public partial class MessageBox : System.Web.UI.UserControl
    {
        String messageBody;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Category("Misc")]
        public String MessageBody
        {
            get { return messageBody; }
            set { messageBody = value; }
        }

        public void DataBind(Message msg)
        {
            lblSender.Text = msg.UserEmail;
            lblMessageBody.Text = msg.MessageBody;
            lblMessageDate.Text = msg.MessageDate.ToString();
            lblMessageID.Text = msg.MessageID.ToString();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //this event is not even being reached when the button is clicked
            int messageID = int.Parse(lblMessageID.Text);
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPDeleteMessage";
            objCommand.Parameters.AddWithValue("@theMessageID", messageID);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }
    }
}