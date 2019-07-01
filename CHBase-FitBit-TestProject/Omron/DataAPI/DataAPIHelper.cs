using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CHBase.Omron.Omron.DataAPI
{
    public class DataAPIHelper
    {

        public const string ENDPOINT_DATA_STAGE = "https://api.omronwellness.com/api/measurement";// "https://ohi-api.numerasocial.com/api/measurement";

        /// <summary>
        /// Use this to retreive user data with token. 
        /// </summary>
        public const string ENDPOINT_GET_DATA = "https://api.health.Omron.com/";

        public const string PARAM_since = "since";

        // The following parameters are optional.

        public const string PARAM_limit = "limit";
        public const string PARAM_type = "type";
        public const string PARAM_includeHourlyActivity = "includeHourlyActivity";
        public const string PARAM_sortOrder = "sortOrder";

        static string _SendRequest(string accessToken, string postData)
        {
            WebRequest req = (HttpWebRequest)WebRequest.Create(ENDPOINT_DATA_STAGE);
            var data = Encoding.ASCII.GetBytes(postData);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers["Authorization"] = String.Format("Bearer {0}", accessToken);

            req.ContentLength = data.Length;

            using (var writer = req.GetRequestStream())
            {
                writer.Write(data, 0, data.Length);
            }

            var resp = req.GetResponse();
            string rawResponseText = new StreamReader(resp.GetResponseStream(), true).ReadToEnd();

            return rawResponseText;
        }

        public static string GetDataFor(APIMethods method, DateTime since, bool includeHourlyActivity = false)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.AppendFormat("{0}={1}", PARAM_since, since.ToShortDateString());

            if (method != APIMethods.all)
            {
                sbData.AppendFormat("&{0}={1}", PARAM_type, method.ToString());
            }

            if (includeHourlyActivity)
            {
                sbData.AppendFormat("&{0}={1}", PARAM_includeHourlyActivity, "true");
            }

            return _SendRequest(OAuthV2.OAuthV2SessionManager.AccessToken, sbData.ToString());

        }
    }
}