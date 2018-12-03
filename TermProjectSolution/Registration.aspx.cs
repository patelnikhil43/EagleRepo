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
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterUserButton_Click(object sender, EventArgs e)
        {
            //Validate

            //Check if email already exist
            String UserEmail = RegisterEmailTxtBox.Text;
          Boolean flag =  CheckIfEmailExist(UserEmail);
            if (flag == true)
            {
                //Alert User that email exist
            }
            else {
                //Register 
                DBConnect dbConnection = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPRegisterUser";
                SqlParameter inputParameter = new SqlParameter("@Email", RegisterEmailTxtBox.Text.ToString());
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.NVarChar;
                objCommand.Parameters.Add(inputParameter);
                inputParameter = new SqlParameter("@Name", RegisterNameTxtBox.Text.ToString());
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                objCommand.Parameters.Add(inputParameter);
                inputParameter = new SqlParameter("@Address", RegisterAddressTxtBox.Text.ToString());
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                objCommand.Parameters.Add(inputParameter);
                inputParameter = new SqlParameter("@City", CityTxtBox.Text.ToString());
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                objCommand.Parameters.Add(inputParameter);
                inputParameter = new SqlParameter("@Zip", int.Parse(ZipTxtBox.Text.ToString()));
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.Int;
                objCommand.Parameters.Add(inputParameter);
                inputParameter = new SqlParameter("@Password", RegisterPasswordTxtBox.Text.ToString());
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;

                objCommand.Parameters.Add(inputParameter);
               int ResponseReceived = dbConnection.DoUpdateUsingCmdObj(objCommand);
                if (ResponseReceived == 1)
                {
                    //User Registered 
                    //Save UserEmail in Session Called UserEmail
                    Session["UserEmail"] = RegisterEmailTxtBox.Text.ToString();
                    RegisterUserDetails.Visible = false;
                    PrivacyQuestionsDiv.Visible = true;
                }
                else {
                    //Error
                }
                
            }
        }
        public Boolean CheckIfEmailExist(String Email) {
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPCheckIfUserExist"; 
            SqlParameter inputParameter = new SqlParameter("@Email", Email);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            DataSet EmailDataSet = dbConnection.GetDataSetUsingCmdObj(objCommand);
            if (EmailDataSet.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        protected void SecurityButton_Click(object sender, EventArgs e)
        {
            //Validate Security Question Answer
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPInsertSecurityQuestion";
            SqlParameter inputParameter = new SqlParameter("@Email", Session["UserEmail"].ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);
            //Question 1
            inputParameter = new SqlParameter("@Q1", PrivacyQ1TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);
            //Answer 1 
            inputParameter = new SqlParameter("@A1", PrivacyA1TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);
            //Question 2
            inputParameter = new SqlParameter("@Q2", PrivacyQ2TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);
            //Answer 2
            inputParameter = new SqlParameter("@A2", PrivacyA2TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);
            //Question 3
            inputParameter = new SqlParameter("@Q3", PrivacyQ3TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);
            //Answer 3
            inputParameter = new SqlParameter("@A3", PrivacyA3TxtBox.Text.ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);

            int ResponseRecevied = dbConnection.DoUpdateUsingCmdObj(objCommand);

            if (ResponseRecevied == 1) {
                //Security Questions Inserted
                PrivacyQuestionsDiv.Visible = false;
                PreferencesDiv.Visible = true;
            }

        }

        protected void SubmitPreferencesButton_Click(object sender, EventArgs e)
        {
            //No Need for Validation
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPInsertUserPreference";
            SqlParameter inputParameter = new SqlParameter("@Email", Session["UserEmail"].ToString());
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@Login", LoginPreferenceDropDown.SelectedValue);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@Theme", ThemePreferenceDropDown.SelectedValue);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@Privacy", PrivacyPreferenceDropDown.SelectedValue);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            int ResponseRecevied = dbConnection.DoUpdateUsingCmdObj(objCommand);

            if (ResponseRecevied == 1) {
                //Preferences Updated 
            }
        }

    }
}