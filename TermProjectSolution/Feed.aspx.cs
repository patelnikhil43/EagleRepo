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
    public partial class Feed : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}