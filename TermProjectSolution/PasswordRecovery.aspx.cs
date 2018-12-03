using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace TermProjectSolution
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetUser_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text != "")
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetUserAccount";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theEmail", txtEmail.Text);
                DataSet myUserAccount = objDB.GetDataSetUsingCmdObj(objCommand);

                if(myUserAccount.Tables[0].Rows.Count > 0)
                { 
                    DataRow account = myUserAccount.Tables[0].Rows[0];
                    if (account[6].ToString() != "")
                    {
                        lblSecurityQ1.Text = account[6].ToString();
                        lblSecurityQ2.Text = account[8].ToString();
                        lblSecurityQ3.Text = account[10].ToString();
                    }
                    else
                    {
                        lblMessage.Text = "You did not create any security questions. Your password will be sent to your email address.";
                    }
                }
            }
        }

        protected void btnSubmitQuestions_Click(object sender, EventArgs e)
        {
            bool allGood = true;

            if (txtEmail.Text == "")
            {
                lblMessage.Text = "Please enter your email and answer each question. Answers are case sensitive.";
                allGood = false;
            }
            if (txtAnswer1.Text == "")
            {
                lblMessage.Text = "Please enter your email and answer each question. Answers are case sensitive.";
                allGood = false;
            }
            if (txtAnswer2.Text == "")
            {
                lblMessage.Text = "Please enter your email and answer each question. Answers are case sensitive.";
                allGood = false;
            }
            if (txtAnswer3.Text == "")
            {
                lblMessage.Text = "Please enter your email and answer each question. Answers are case sensitive.";
                allGood = false;
            }


            if (allGood)
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "RecoverPassword";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theEmail", txtEmail.Text);
                objCommand.Parameters.AddWithValue("@theAnswer1", txtAnswer1.Text);
                objCommand.Parameters.AddWithValue("@theAnswer2", txtAnswer2.Text);
                objCommand.Parameters.AddWithValue("@theAnswer3", txtAnswer3.Text);

                DataSet myPassword = objDB.GetDataSetUsingCmdObj(objCommand);

                if(myPassword.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Text = "Your password is: " + myPassword.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    lblMessage.Text = "Error: Recheck your email address and the answers to your security questions and try again.";
                }
            }
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}