﻿using System;
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
    public partial class Feed : System.Web.UI.Page
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

        protected void ChoosePostTypeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChoosePostTypeDD.SelectedValue == "PhotoPost")
            {
                TypeImagePostDiv.Visible = true;
                TypeStatusPostDiv.Visible = false;
            }
            if (ChoosePostTypeDD.SelectedValue == "StatusPost")
            {
                TypeStatusPostDiv.Visible = true;
                TypeImagePostDiv.Visible = false;
            }
        }

        protected void PostButton_Click(object sender, EventArgs e)
        {

        }
    }
}