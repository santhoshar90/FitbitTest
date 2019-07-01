using CHBase_FitBit_TestProject.Garmin.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CHBase_FitBit_TestProject.Garmin.Pages
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void aStart_ServerClick(object sender, EventArgs e)
        {
            //    var signature = GenerateOAuthSignature("GET&" + UpperCaseUrlEncode((BASE_URL + PATH_REQUEST_TOKEN).ToLower()) +
            //        "&" + UpperCaseUrlEncode(""));

            string strParamters = "oauth_consumer_key=37d14781-5529-4a3a-9c55-aad6b835913c&oauth_nonce=kbki9sCGRwU&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1543562022&oauth_version=1.0";
            string strRequestUrl = "POST&" + AuthHelper.UpperCaseUrlEncode(("https://connectapi.garmin.com/oauth-service/oauth/request_token").ToLower()) + "&" + AuthHelper.UpperCaseUrlEncode(strParamters);


            string HashKey = "fq07vfP6JodQr0EmgnPYUxKPkNNv8pKoib6" + "&";

            var signature = AuthHelper.GenerateOAuthSignature(HashKey,strRequestUrl);

            strRequestUrl = strRequestUrl + AuthHelper.UpperCaseUrlEncode(signature);


            string PostUrl = "https://connectapi.garmin.com/oauth-service/oauth/request_token";
            Hashtable htKeys = GenerateHasTable(signature);

            string AuthHeader = "oauth_version=\"1.0\", oauth_consumer_key=\"37d14781-5529-4a3a-9c55-aad6b835913c\", oauth_timestamp=\"1543562022\", oauth_nonce=\"kbki9sCGRwU\", oauth_signature_method=\"HMAC-SHA1\", oauth_signature=\"" + signature+"\"";
            try
            {
                HttpClient hc = new HttpClient();
                hc.DefaultRequestHeaders.Add("Authorization", "OAuth " + AuthHeader);

                HttpResponseMessage response = hc.PostAsync(PostUrl, null).Result;

                string str = response.Content.ReadAsStringAsync().Result;

                string strOAuthToken = str.Split('&')[0];
                string strOAuthTokenSecret = str.Split('&')[1];

                strOAuthToken = strOAuthToken.Split('=')[1];
                strOAuthTokenSecret = strOAuthTokenSecret.Split('=')[1];

                HttpContext.Current.Session["OAuthToken_Secret"] = strOAuthTokenSecret;

                string AuthCallBack = "http://dit.dev.grcdemo.com/Garmin/Pages/Callback.aspx";

                Response.Redirect("https://connect.garmin.com/oauthConfirm?oauth_token=" + strOAuthToken + "&oauth_callback=" + AuthCallBack);



            }
            catch (AggregateException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        private Hashtable GenerateHasTable(string signature)
        {
            Hashtable htKeys = new Hashtable();
            htKeys.Add("oauth_consumer_key", "37d14781-5529-4a3a-9c55-aad6b835913c");
            htKeys.Add("oauth_nonce", "kbki9sCGRwU");
            htKeys.Add("oauth_signature_method", "HMACSHA1");
            htKeys.Add("oauth_timestamp", "1543562022");
            htKeys.Add("oauth_version", "1.0");
            htKeys.Add("oauth_signature", signature);

            return htKeys;

        }
    }
}