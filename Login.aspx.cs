using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;



public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie user = Request.Cookies.Get(Login1.UserName);
      
    }    

    protected void LoginSatus1_Logout(object sender, LoginCancelEventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("");
    }

    protected void Login1_Authenticate1(object sender, AuthenticateEventArgs e)
    {
        if (Membership.ValidateUser(Login1.UserName, Login1.Password))
        {
            FormsAuthentication.SetAuthCookie(Login1.UserName, true);
            HttpCookie user = Request.Cookies.Get(Login1.UserName);
            user.Expires = DateTime.Now.AddDays(-1);            
            Response.Redirect("~/Home.aspx");

        }
    }
}