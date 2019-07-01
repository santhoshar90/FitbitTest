using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.iHealth
{
    public partial class Callback : System.Web.UI.Page
    {
        public string AuthCode
        {
            get
            {
                if (!String.IsNullOrEmpty(Request["code"]))
                {
                    return Request["code"];
                }
                return string.Empty;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string AuthTokenUrl = "https://api.ihealthlabs.com:8443/OpenApiV2/OAuthv2/userauthorization/?client_id=aed3134f0a36458ca45d4eb195d68ccc&client_secret=1550bd3c107f46bc8d0f71966881dfd1&grant_type=authorization_code&redirect_uri=https%3A%2F%2Ffitbittest.dev.grcdemo.com%2FiHealth%2FCallback.aspx&code=" + AuthCode;
            
        }

        //private Dictionary<string, string> _CreateToken(Dictionary<string, string> values)
        //{
        //    string redirectUrl = "http://fitbitchbasetest.dev19.grcdev.com/Pages/CallBack.aspx";
        //    string tokenURI = "https://api.fitbit.com/oauth2/token";
        //    string myParameters = "client_id=22CV6D&grant_type=authorization_code&redirect_uri=" + redirectUrl + "&code=" + AuthCode;
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("22CV6D:fe8e45d401fec4bd5b340b0b09772f7d");
        //    string strAuthHeader = System.Convert.ToBase64String(plainTextBytes);

        //    values = new Dictionary<string, string>();
        //    using (WebClient wc = new WebClient())
        //    {
        //        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        //        //wc.Headers["Authorization"] = "Basic MjJDVjZEOmZlOGU0NWQ0MDFmZWM0YmQ1YjM0MGIwYjA5NzcyZjdk";
        //        wc.Headers[HttpRequestHeader.Authorization] = "Basic MjJDVjZEOmZlOGU0NWQ0MDFmZWM0YmQ1YjM0MGIwYjA5NzcyZjdk";
        //        string TokenResult = wc.UploadString(tokenURI, myParameters);
        //        values = JsonConvert.DeserializeObject<Dictionary<string, string>>(TokenResult);
        //    }

        //    return values;
        //}
    }
}