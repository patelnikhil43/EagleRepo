using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectSolution
{
    public partial class LoginPictures : System.Web.UI.Page
    {
        ArrayList loginPictures = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            loginPictures.Add("https://st.depositphotos.com/1005920/1294/i/450/depositphotos_12946417-stock-photo-login-icon.jpg");
            loginPictures.Add("https://www.facebook.com/images/fb_icon_325x325.png");
            loginPictures.Add("https://cdn1-www.dogtime.com/assets/uploads/2010/12/puppies.jpg");

            Random rand = new Random();

            int num = rand.Next(0, 2);

            Response.Write(loginPictures[num].ToString());
        }
    }
}