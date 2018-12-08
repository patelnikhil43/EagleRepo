﻿using System;
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

        }

        public void DataBind(Posts post)
        {
            lblPoster.Text = post.UserEmail;
            lblPostCaption.Text = post.PostBody;
            lblPostDate.Text = post.DatePosted.ToString();
            lblPostID.Text = post.PostID.ToString();
        }
    }
}