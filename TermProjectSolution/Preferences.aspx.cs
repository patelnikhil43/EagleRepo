using System;
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
    public partial class Preferences : System.Web.UI.Page
    {
        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };


        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
        }

        protected void SubmitPreferencesButton_Click(object sender, EventArgs e)
        {
            //No Need for Validation
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPUpdateUserPreference";
            SqlParameter inputParameter = new SqlParameter("@Email", Session["userEmail"].ToString());
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

            inputParameter = new SqlParameter("@PhotoPrivacy", PhotoPrivacyDropDown.SelectedValue);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.NVarChar;
            objCommand.Parameters.Add(inputParameter);

            int ResponseRecevied = dbConnection.DoUpdateUsingCmdObj(objCommand);

            Settings userSettings = new Settings();
            userSettings.LoginPreference = LoginPreferenceDropDown.SelectedValue;
            userSettings.Theme = ThemePreferenceDropDown.SelectedValue;
            userSettings.ProfileInfoPrivacy = PrivacyPreferenceDropDown.SelectedValue;
            userSettings.PhotoPrivacy = PhotoPrivacyDropDown.SelectedValue;
            userSettings.FeedPrivacy = FeedPrivacyDropDown.SelectedValue;

            Session.Add("userSettings", userSettings);

            //BinaryFormatter serializer = new BinaryFormatter();
            //MemoryStream memoryStream = new MemoryStream();
            //serializer.Serialize(memoryStream, userSettings);
            //Byte[] byteSettings = memoryStream.ToArray();

            //objCommand.CommandType = CommandType.StoredProcedure;
            //objCommand.CommandText = "TPUpdateUserSettings";

            //objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
            //objCommand.Parameters.AddWithValue("@theSettings", byteSettings);

            //int ResponseRecevied = dbConnection.DoUpdateUsingCmdObj(objCommand);

            //if (ResponseRecevied == 1)
            //{
            //    Response.Write("Settings updated successfully.");
            //}
            //else
            //{
            //    Response.Write("There was a problem updating your user settngs.");
            //}

            String plainTextEmail = Session["userEmail"].ToString();
            String plainTextPassword = Session["userPassword"].ToString();
            String encryptedEmail;
            String encryptedPassword;
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] emailBytes;
            Byte[] passwordBytes;

            emailBytes = encoder.GetBytes(plainTextEmail);
            passwordBytes = encoder.GetBytes(plainTextPassword);
            RijndaelManaged rmEncryption = new RijndaelManaged();
            MemoryStream memStream = new MemoryStream();
            CryptoStream encryptionStream = new CryptoStream(memStream, rmEncryption.CreateEncryptor(key, vector), CryptoStreamMode.Write);


            if (ResponseRecevied == 1)
            {
                //Preferences Updated 
                if (LoginPreferenceDropDown.SelectedValue == "NONE")
                {
                    //Do Nothing
                    Response.Redirect("Feed.aspx");
                }
                if (LoginPreferenceDropDown.SelectedValue == "Auto-Login")
                {
                    //Auto Login
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
                    Response.Redirect("Feed.aspx");
                }
                if (LoginPreferenceDropDown.SelectedValue == "Fast-Login")
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
                    Response.Redirect("Feed.aspx");
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            DBConnect dbConnection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TPUpdateStatusLogout";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());

            dbConnection.DoUpdateUsingCmdObj(objCommand);
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}