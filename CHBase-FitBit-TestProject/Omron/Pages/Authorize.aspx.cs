using CHBase.Omron.Omron.OAuthV2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase.Omron.Omron.Pages
{
    public partial class Authorize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string client_id = ConfigurationManager.AppSettings["AppKey"];
            string client_secret = ConfigurationManager.AppSettings["AppSecret"];
            string redirect_url = ConfigurationManager.AppSettings["CallbackUrl"];

            OAuthV2Authenticate oauth = new OAuthV2Authenticate(client_id, client_secret, redirect_url);
            lnkSignIn.NavigateUrl = oauth.GetAuthorizationLink();
        }
    }
}