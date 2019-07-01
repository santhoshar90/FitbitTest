using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Pages
{
   
    public partial class Notifications : System.Web.UI.Page
    {
        public string VerificationCode
        {
            get
            {
                if (!String.IsNullOrEmpty(Request["verify"]))
                {
                    return Request["verify"];
                }
                return string.Empty;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(VerificationCode))
            {
                if(VerificationCode == "9cd16f797f4195a10178693fd665bf8a822fbe6edef8fad3974f2d2f2c6ec7a5")
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;

                else
                    base.Response.StatusCode = (int)HttpStatusCode.NotFound;

            }
        }
    }
}