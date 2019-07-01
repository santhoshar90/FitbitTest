using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Pages
{
    public partial class StartupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkFitbit_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=22CV6D&redirect_uri=http%3A%2F%2Ffitbitchbasetest.dev19.grcdev.com%2FPages%2FCallBack.aspx&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight");
        }

        protected void lnkOmron_ServerClick(object sender, EventArgs e)
        {

        }
    }
}