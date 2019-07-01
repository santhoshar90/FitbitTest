using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.iHealth
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkStart_ServerClick(object sender, EventArgs e)
        {
            string RedirectUrl = @"https://api.ihealthlabs.com:8443/OpenApiV2/OAuthv2/userauthorization/?client_id=aed3134f0a36458ca45d4eb195d68ccc&response_type=code&redirect_uri=http%3A%2F%2Ffitbitchbasetest.dev19.grcdev.com%2FiHealth%2FCallback.aspx&APIName=OpenApiSleep%C2%A0OpenApiBP%C2%A0OpenApiWeight%C2%A0OpenApiBG%C2%A0OpenApiSpO2%C2%A0OpenApiActivity%C2%A0OpenApiUserInfo%C2%A0OpenApiFood%C2%A0OpenApiSport&IsNew=false";
           

            Response.Redirect(RedirectUrl);
        }
    }
}