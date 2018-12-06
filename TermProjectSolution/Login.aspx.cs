using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

using System.Security.Cryptography;     // needed for the encryption classes
using System.IO;                        // needed for the MemoryStream
using System.Text;                      // needed for the UTF8 encoding
using System.Net;                       // needed for the cookie
using System.Runtime.Serialization.Formatters.Binary;

namespace TermProjectSolution
{
    public partial class Login : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        private Byte[] key = { 250, 101, 18, 76, 45, 135, 207, 118, 4, 171, 3, 168, 202, 241, 37, 199 };
        private Byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes["type"] = "password";
            rdoNormalLogin.Checked = true;
            // Read encrypted password from cookie
            if (!IsPostBack && Request.Cookies["LoginCookie"] != null)
            {
                HttpCookie myCookie = Request.Cookies["LoginCookie"];
                //txtEmail.Text = myCookie.Values["Email"];
                //txtPassword.Text = myCookie.Values["Password"];
                String encryptedEmail = myCookie.Values["Email"];
                String encryptedPassword = myCookie.Values["Password"];

                Byte[] encryptedEmailBytes = Convert.FromBase64String(encryptedEmail);
                Byte[] emailBytes;
                String plainTextEmail;

                UTF8Encoding encoder = new UTF8Encoding();

                RijndaelManaged rmEncryption = new RijndaelManaged();
                MemoryStream memStream = new MemoryStream();
                CryptoStream decryptionStream = new CryptoStream(memStream, rmEncryption.CreateDecryptor(key, vector), CryptoStreamMode.Write);

                //Email
                decryptionStream.Write(encryptedEmailBytes, 0, encryptedEmailBytes.Length);
                decryptionStream.FlushFinalBlock();

                memStream.Position = 0;
                emailBytes = new Byte[memStream.Length];
                memStream.Read(emailBytes, 0, emailBytes.Length);

                decryptionStream.Close();
                memStream.Close();

                plainTextEmail = encoder.GetString(emailBytes);
                txtEmail.Text = plainTextEmail;

                //Password
                if (encryptedPassword != null)
                {
                    Byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                    Byte[] passwordBytes;
                    String plainTextPassword;

                    memStream = new MemoryStream();
                    decryptionStream = new CryptoStream(memStream, rmEncryption.CreateDecryptor(key, vector), CryptoStreamMode.Write);

                    decryptionStream.Write(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                    decryptionStream.FlushFinalBlock();

                    memStream.Position = 0;
                    passwordBytes = new Byte[memStream.Length];
                    memStream.Read(passwordBytes, 0, passwordBytes.Length);

                    decryptionStream.Close();
                    memStream.Close();

                    plainTextPassword = encoder.GetString(passwordBytes);
                    txtPassword.Text = plainTextPassword;
                    //lblMessage.Text = "Password: " + plainTextPassword;
                }

                if (txtPassword.Text != "")
                {
                    rdoAutoLogin.Checked = true;
                }
                else
                {
                    rdoFastLogin.Checked = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //validate textboxes; done!
            //check if valid user; done!
            //check if there is a username and/or password saved in cookies
            //store userAccount in Session so they can't access all pages by typing in URL
            //maybe add radiobuttons to select which type of login??
            bool allGood = true;
            if(txtEmail.Text == "")
            {
                allGood = false;
                lblMessage.Text = "You must enter a username and password in the boxes below.";
            }
            if(txtPassword.Text == "")
            {
                allGood = false;
                lblMessage.Text = "You must enter a username and password in the boxes below.";
            }
            if (allGood)
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CheckUserAccount";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theEmail", txtEmail.Text);
                objCommand.Parameters.AddWithValue("@thePassword", txtPassword.Text);

                DataSet myAccount = objDB.GetDataSetUsingCmdObj(objCommand);

                //LOGIN SUCCESSFUL
                if (myAccount.Tables[0].Rows.Count > 0)
                {
                    lblMessage.Text = "You logged in good job!";
                    //might need to switch this to a UserAccount Object, not sure tho
                    Session.Add("userEmail", txtEmail.Text);
                    Session.Add("userPassword", txtPassword.Text);

                    if(objDB.GetField("userSettings", 0) != System.DBNull.Value)
                    {
                        Byte[] byteSettings = (Byte[])objDB.GetField("userSettings", 0);
                        BinaryFormatter deserializer = new BinaryFormatter();
                        MemoryStream memoryStream = new MemoryStream(byteSettings);

                        Settings userSettings = (Settings)deserializer.Deserialize(memoryStream);
                        Session.Add("userSettings", userSettings);
                    }

                    String plainTextEmail = txtEmail.Text;
                    String plainTextPassword = txtPassword.Text;
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

                    bool hadSettings = false;
                    Settings loginSettings = new Settings();
                    if(Session["userSettings"] != null)
                    {
                        loginSettings = (Settings)Session["userSettings"];
                        hadSettings = true;
                    }
                    else
                    {
                        loginSettings.Theme = "Default";
                        loginSettings.PersonalContactInfoPrivacy = "Friends";
                        loginSettings.PhotoPrivacy = "Friends";
                        loginSettings.ProfileInfoPrivacy = "Friends";
                    }

                    if (rdoAutoLogin.Checked)
                    {
                        if (hadSettings)
                        {
                            loginSettings.LoginPreference = "Auto-Login";
                            Session.Add("userSettings", loginSettings);
                        }
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
                    else if (rdoFastLogin.Checked)
                    {
                        if (hadSettings)
                        {
                            loginSettings.LoginPreference = "Fast-Login";
                            Session.Add("userSettings", loginSettings);
                        }
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
                    else
                    {
                        if (hadSettings)
                        {
                            loginSettings.LoginPreference = "None";
                            Session.Add("userSettings", loginSettings);
                        }
                        //delete cookies from computer
                        if (Request.Cookies["LoginCookie"] != null)
                        {
                            Response.Cookies.Remove("LoginCookie");
                        }
                        
                    }
                    //i dont think i need this
                    //HttpCookie emailCookie = new HttpCookie("EmailCookie");
                    //emailCookie.Values["Email"] = txtEmail.Text;
                    //Response.Cookies.Add(emailCookie);
                    objCommand.CommandText = "TPUpdateStatusLogin";
                    objCommand.Parameters.Clear();
                    objCommand.Parameters.AddWithValue("@theUserEmail", txtEmail.Text);

                    objDB.DoUpdateUsingCmdObj(objCommand);
                    Response.Redirect("Feed.aspx");
                }
                else
                {
                    lblMessage.Text = "Error: username or password incorrect";
                }
            }
        }
    }
}