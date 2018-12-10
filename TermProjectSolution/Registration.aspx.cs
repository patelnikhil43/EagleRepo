using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace TermProjectSolution
{
    public partial class Registration : System.Web.UI.Page
    {
        ArrayList UserRegistrationError = new ArrayList();
        ArrayList UserRegistrationSecurityAnswers = new ArrayList();
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void ValidateUserRegistration()
        {
            if (RegisterNameTxtBox.Text == "")
            {
                UserRegistrationError.Add("Enter Name");

            }
            if (RegisterAddressTxtBox.Text == "")
            {
                UserRegistrationError.Add("Enter Address");
            }
            if (CityTxtBox.Text == "") 
            {
                UserRegistrationError.Add("Enter City");
            }
            if (StateTxtBox.Text == "") 
            {
                UserRegistrationError.Add("Enter State");
            }
            if (StateTxtBox.Text.Length != 2)
            {
                UserRegistrationError.Add("State is 2 Characters like PA, NY or FL");
            }
            if (ZipTxtBox.Text == "" || ZipTxtBox.Text.Length < 5 || ZipTxtBox.Text.Length > 5)
            {
                UserRegistrationError.Add("Enter Valid Zip Length is 5");
            }
            if (RegisterEmailTxtBox.Text == "")
            {
                UserRegistrationError.Add("Enter Email");
            }
            if (RegisterPasswordTxtBox.Text == "" && RegisterCPasswordTxtBox.Text == "")
            {
                UserRegistrationError.Add("Enter Password");
            }
            if (RegisterPasswordTxtBox.Text != RegisterCPasswordTxtBox.Text)
            {
                UserRegistrationError.Add("Password Don't Match");
            }

        }

        protected void RegisterUserButton_Click(object sender, EventArgs e)
        {
            //Validate
            ValidateUserRegistration();

            if (UserRegistrationError.Count == 0) {
                //Check if email already exist
                String UserEmail = RegisterEmailTxtBox.Text;
                Boolean flag = CheckIfEmailExist(UserEmail);
                if (flag == true)
                {
                    Response.Write("Email already exist");
                }
                else
                {
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
                    inputParameter = new SqlParameter("@State", StateTxtBox.Text.ToString());
                    inputParameter.Direction = ParameterDirection.Input;
                    inputParameter.SqlDbType = SqlDbType.VarChar;
                    objCommand.Parameters.Add(inputParameter);

                   
                    int ResponseReceived = dbConnection.DoUpdateUsingCmdObj(objCommand);
                    if (ResponseReceived == 1)
                    {
                        //User Registered 
                        //Save UserEmail in Session Called UserEmail
                        Session.Add("userEmail", RegisterEmailTxtBox.Text.ToString());
                        RegisterUserDetails.Visible = false;
                        PrivacyQuestionsDiv.Visible = true;

                    }
                    else
                    {
                        Response.Write("Error Occured on the DATABASE");
                    }

                }
            }
            else
            {
                for (int i = 0; i < UserRegistrationError.Count; i++)
                {
                    Response.Write(UserRegistrationError[i] + "<br/>");
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

        void ValidateUserQuestions()
        {
            if (PrivacyQ1TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Question 1");

            }
            if (PrivacyA1TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Answer 1");

            }
            if (PrivacyQ2TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Question 2");

            }
            if (PrivacyA2TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Answer 2");

            }
            if (PrivacyQ3TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Question 3");

            }
            if (PrivacyA3TxtBox.Text == "")
            {
                UserRegistrationSecurityAnswers.Add("Enter Answer 3");

            }
        }

        protected void SecurityButton_Click(object sender, EventArgs e)
        {
            ValidateUserQuestions();
            //Validate Security Question Answer

            if (UserRegistrationSecurityAnswers.Count == 0)
            {

                DBConnect dbConnection = new DBConnect();
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPInsertSecurityQuestion";
                SqlParameter inputParameter = new SqlParameter("@Email", Session["userEmail"].ToString());
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

                if (ResponseRecevied == 1)
                {
                    //Security Questions Inserted
                    PrivacyQuestionsDiv.Visible = false;
                    PreferencesDiv.Visible = true;
                }
            }
            else
            {

                for (int i = 0; i < UserRegistrationSecurityAnswers.Count; i++)
                {
                    Response.Write(UserRegistrationSecurityAnswers[i] + "<br/>");
                }
            }


        }

        protected void SubmitPreferencesButton_Click(object sender, EventArgs e)
        {
            //No Need for Validation
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPInsertUserPreference";
            objCommand.Parameters.AddWithValue("@Email", Session["userEmail"].ToString());
            objCommand.Parameters.AddWithValue("@Login", LoginPreferenceDropDown.SelectedValue);
            objCommand.Parameters.AddWithValue("@Theme", ThemePreferenceDropDown.SelectedValue);
            objCommand.Parameters.AddWithValue("@ProfileInfoPrivacy", PrivacyPreferenceDropDown.SelectedValue);
            objCommand.Parameters.AddWithValue("@PhotoPrivacy", PhotoPrivacyDropDown.SelectedValue);
            objCommand.Parameters.AddWithValue("@FeedPrivacy", FeedPrivacyDropDown.SelectedValue);


            int ResponseRecevied = dbConnection.DoUpdateUsingCmdObj(objCommand);

            Settings userSettings = new Settings();
            userSettings.LoginPreference = LoginPreferenceDropDown.SelectedValue;
            userSettings.Theme = ThemePreferenceDropDown.SelectedValue;
            userSettings.ProfileInfoPrivacy = PrivacyPreferenceDropDown.SelectedValue;
            userSettings.PhotoPrivacy = PhotoPrivacyDropDown.SelectedValue;
            userSettings.FeedPrivacy = FeedPrivacyDropDown.SelectedValue;

            Session.Add("userSettings", userSettings);

            String plainTextEmail = RegisterEmailTxtBox.Text;
            String plainTextPassword = RegisterPasswordTxtBox.Text;
            String encryptedEmail;
            String encryptedPassword;

            System.Text.UTF8Encoding encoder = new UTF8Encoding();
            Byte[] emailBytes;
            Byte[] passwordBytes;

            emailBytes = encoder.GetBytes(plainTextEmail);
            passwordBytes = encoder.GetBytes(plainTextPassword);

            RijndaelManaged rmEncryption = new RijndaelManaged();
            MemoryStream memStream = new MemoryStream();
            CryptoStream encryptionStream = new CryptoStream(memStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

            if(userSettings.LoginPreference == "Auto-Login")
            {
                //Email
                encryptionStream.Write(emailBytes, 0, emailBytes.Length);
                encryptionStream.FlushFinalBlock();

                memStream.Position = 0;
                Byte[] encryptedEmailBytes = new byte[memStream.Length];
                memStream.Read(encryptedEmailBytes, 0, encryptedEmailBytes.Length);

                encryptionStream.Close();
                memStream.Close();

                //password
                memStream = new MemoryStream();
                encryptionStream = new CryptoStream(memStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);

                encryptionStream.Write(passwordBytes, 0, passwordBytes.Length);
                encryptionStream.FlushFinalBlock();

                memStream.Position = 0;
                Byte[] encryptedPasswordBytes = new byte[memStream.Length];
                memStream.Read(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);

                encryptionStream.Close();
                memStream.Close();

                encryptedEmail = Convert.ToBase64String(encryptedEmailBytes);
                encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);

                HttpCookie myCookie = new HttpCookie("LoginCookie");
                myCookie.Values["Email"] = encryptedEmail;
                myCookie.Expires = new DateTime(2020, 2, 1);
                myCookie.Values["Password"] = encryptedPassword;
                myCookie.Expires = new DateTime(2020, 2, 1);
                Response.Cookies.Add(myCookie);
            }
            else if(userSettings.LoginPreference == "Fast-Login")
            {
                encryptionStream.Write(emailBytes, 0, emailBytes.Length);
                encryptionStream.FlushFinalBlock();

                memStream.Position = 0;
                Byte[] encryptedEmailBytes = new byte[memStream.Length];
                memStream.Read(encryptedEmailBytes, 0, encryptedEmailBytes.Length);

                encryptionStream.Close();
                memStream.Close();

                encryptedEmail = Convert.ToBase64String(encryptedEmailBytes);

                HttpCookie myCookie = new HttpCookie("LoginCookie");
                myCookie.Values["Email"] = encryptedEmail;
                myCookie.Expires = new DateTime(2020, 2, 1);
                Response.Cookies.Add(myCookie);
            }
            
            Response.Redirect("Feed.aspx");

            //the code below happens in the session end event in the global.asax file

            //BinaryFormatter serializer = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //serializer.Serialize(memStream, userSettings);
            //Byte[] byteSettings = memStream.ToArray();

            //objCommand.CommandType = CommandType.StoredProcedure;
            //objCommand.CommandText = "TPUpdateUserSettings";

            //objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
            //objCommand.Parameters.AddWithValue("@theSettings", byteSettings);

            //int retVal = dbConnection.DoUpdateUsingCmdObj(objCommand);

            //if (ResponseRecevied == 1) {
            //    //Preferences Updated 
            //}

            //if(retVal == 1)
            //{
            //    Response.Write("Settings updated successfully.");
            //}
            //else
            //{
            //    Response.Write("There was a problem updating your user settngs.");
            //}
        }

    }
}