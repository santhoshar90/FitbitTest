using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CHBase.Omron.Omron.OAuthV2
{
    public static class OAuthV2RequestHelper
    {
        public static OAuthV2ResponseObject SendRequest(Uri endpoint, string postData)
        {
            WebRequest req = (HttpWebRequest) WebRequest.Create(endpoint);
            var data = Encoding.ASCII.GetBytes(postData);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;

            using (var writer = req.GetRequestStream())
            {
                writer.Write(data, 0, data.Length);
            }

            var resp = req.GetResponse();
            string rawResponseText = new StreamReader(resp.GetResponseStream(), true).ReadToEnd();
            OAuthV2ResponseObject oauthV2Response = new OAuthV2ResponseObject(rawResponseText);

            return oauthV2Response;
        }
    }

    [Serializable]
    public class OAuthV2ResponseObject
    {
        public string Error { get; set; }

        public string RawResponse { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        
        public string ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string Scope { get; set; }

        /// <summary>
        /// Will be a JWT that contains information about user.
        /// </summary>
        public string IdToken { get; set; }

        public string UserId { get; set; }

        public OAuthV2ResponseObject(string rawResponse)
        {
            var dictResponseObject = JsonConvert.DeserializeObject<Dictionary<String, String>>(rawResponse);

            RawResponse = rawResponse;
            AccessToken = dictResponseObject["access_token"];
            RefreshToken = dictResponseObject["refresh_token"];
            ExpiresIn = dictResponseObject["expires_in"];
            TokenType = dictResponseObject["token_type"];
            IdToken = dictResponseObject["id_token"];

            // Will need to add JWT validation.

            // Get User ID.
            //var t = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            //var jToken = t.ReadJwtToken(IdToken);
            //UserId = jToken.Subject;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var property in this.GetType().GetProperties())
            {
                sb.AppendFormat("{0}={1}</br>", property.Name, property.GetValue(this));
            }

            return sb.ToString();
        }
    }
}