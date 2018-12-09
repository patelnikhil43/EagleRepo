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
    public partial class UpdateProfile : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //validation
            bool allGood = true;
            if(txtName.Text == "")
            {
                lblName.Text = "You must enter a name.";
                allGood = false;
            }
            else
            {
                lblName.Text = "Name";
            }
            if (txtAddress.Text == "")
            {
                lblAddress.Text = "You must enter an address.";
                allGood = false;
            }
            else
            {
                lblAddress.Text = "Address";
            }
            if (txtCity.Text == "")
            {
                lblCity.Text = "You must enter a city name.";
                allGood = false;
            }
            else
            {
                lblCity.Text = "City";
            }
            int temp;
            if(txtZip.Text.Length != 5)
            {
                lblZip.Text = "You must enter a valid zip code.";
                allGood = false;
            }
            else if(int.TryParse(txtZip.Text, out temp))
            {
                if (temp < 0)
                {
                    lblZip.Text = "You must enter a positive zip code.";
                    allGood = false;
                }
                else
                {
                    lblZip.Text = "Zip";
                }
            }
            else
            {
                lblZip.Text = "Zip";
            }
            if (allGood)
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "TPUpdateProfileInfo";
                objCommand.Parameters.Clear();

                objCommand.Parameters.AddWithValue("@theUserEmail", Session["userEmail"].ToString());
                objCommand.Parameters.AddWithValue("@theName", txtName.Text);
                objCommand.Parameters.AddWithValue("@theAddress", txtAddress.Text);
                objCommand.Parameters.AddWithValue("@theCity", txtCity.Text);
                objCommand.Parameters.AddWithValue("@theZip", txtZip.Text);

                int retVal = objDB.DoUpdateUsingCmdObj(objCommand);

                if(retVal == 1)
                {
                    lblMessage.Text = "Profile successfully updated!";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Profile.aspx");
        }
    }
}