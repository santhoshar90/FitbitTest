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
    public partial class CallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string client_id = ConfigurationManager.AppSettings["AppKey"];
            string client_secret = ConfigurationManager.AppSettings["AppSecret"];
            string redirect_url = ConfigurationManager.AppSettings["CallbackUrl"]; 

            OAuthV2Authenticate oauth = new OAuthV2Authenticate(client_id, client_secret, redirect_url);
            OAuthV2ResponseObject response = null;
            try
            {
                response = oauth.ManageUserAuthCallback();
                litResponse1.Text = response.ToString();
            }
            catch(Exception ex)
            {
                litResponse2.Text = "Exception occured : " + ex.ToString();
                btnSignUP.Visible = true;
                return;
            }

            if (!OAuthV2.OAuthV2SessionManager.IsAccessTokenSet() || response == null)
            {
                litResponse2.Text = "No access token in session";
                btnSignUP.Visible = true;
            }

            // If everything is good. Save tokens to DB and Redirect to Data page.
            DBHelper.Create(response);
            Response.Redirect("/Omron/Pages/Data.aspx");

        }

        protected void btnSignUP_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Omron/Pages/Authorize.aspx");
        }
    }
}