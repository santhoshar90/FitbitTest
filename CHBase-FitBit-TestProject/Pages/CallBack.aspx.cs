using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Pages
{
    public partial class CallBack : System.Web.UI.Page
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
            litAuthCode.Text = AuthCode;
        }

        protected void lnkFetchAllData_ServerClick(object sender, EventArgs e)
        {
            //need to fetch all weight data from fitbit. Save this in a file.

            //Auth Code is obtained 
            Dictionary<string, string> values = new Dictionary<string, string>();
            if ((Session["Auth_Token"] == null))
            {
                values = _CreateToken(values);
            }
            Session["Auth_Token"] = values["access_token"];
            string fitbitActivityUrl = "https://api.fitbit.com/1/user/-/activities/list.json?limit=10&sort=asc&afterDate=2018-03-15T00:00:00&offset=0";
            //using (WebClient wc = new WebClient())
            //{
            //    myParameters = "";
            //    //wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //    //wc.Headers["Authorization"] = "Basic MjJDVjZEOmZlOGU0NWQ0MDFmZWM0YmQ1YjM0MGIwYjA5NzcyZjdk";
            //    wc.Headers[HttpRequestHeader.Authorization] = "Bearer "+values["access_token"];
            //    wc.
            //    string ActivityResult = wc.UploadString(fitbitActivityUrl, "GET", myParameters);

            //}

            HttpClient hc = new HttpClient();
            hc.DefaultRequestHeaders.Add("Authorization", "Bearer " + Session["Auth_Token"]);
            HttpResponseMessage response = hc.GetAsync(fitbitActivityUrl).Result;
            
            string str = response.Content.ReadAsStringAsync().Result;

            string strFIleSavePath = ConfigurationManager.AppSettings["FileSavePath"] + DateTime.Now.ToFileTime().ToString() + ".txt";
            File.WriteAllText(strFIleSavePath, str);


        }

        private Dictionary<string, string> _CreateToken(Dictionary<string, string> values)
        {
            string redirectUrl = "http://fitbitchbasetest.dev19.grcdev.com/Pages/CallBack.aspx";
            string tokenURI = "https://api.fitbit.com/oauth2/token";
            string myParameters = "client_id=22CV6D&grant_type=authorization_code&redirect_uri=" + redirectUrl + "&code=" + AuthCode;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("22CV6D:fe8e45d401fec4bd5b340b0b09772f7d");
            string strAuthHeader = System.Convert.ToBase64String(plainTextBytes);

            values = new Dictionary<string, string>();
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                //wc.Headers["Authorization"] = "Basic MjJDVjZEOmZlOGU0NWQ0MDFmZWM0YmQ1YjM0MGIwYjA5NzcyZjdk";
                wc.Headers[HttpRequestHeader.Authorization] = "Basic MjJDVjZEOmZlOGU0NWQ0MDFmZWM0YmQ1YjM0MGIwYjA5NzcyZjdk";
                string TokenResult = wc.UploadString(tokenURI, myParameters);
                values = JsonConvert.DeserializeObject<Dictionary<string, string>>(TokenResult);
            }

            return values;
        }
    }
}
