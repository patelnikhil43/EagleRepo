using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace TermProjectSolution
{
    public partial class ProfileFeed : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["userEmail"] as string))
            {
                Response.Redirect("NoAccess.aspx");
            }
        }

        public void DataBind(Posts post)
        {
            lblPoster.Text = post.UserEmail;
            lblPostCaption.Text = post.PostBody;
            lblPostDate.Text = post.DatePosted.ToString();
            lblPostID.Text = post.PostID.ToString();
            if (post.ImageURL.ToString() == "")
            {
                imgPicture.Visible = false;
            }
            else
            {
                imgPicture.ImageUrl = "../Storage/" + post.ImageURL.ToString();
            }
        }
    }
}