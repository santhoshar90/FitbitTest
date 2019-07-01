using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CHBase.Omron.Omron.OAuthV2
{
    public class OAuthV2Authenticate
    {

        public const string ENDPOINT_STAGE = "https://oauth.omronwellness.com"; //"https://ohi-oauth.numerasocial.com";

        /// <summary>
        /// Use this to get your access code.
        /// </summary>
        public const string ENDPOINT_AUTHORIZATION          = ENDPOINT_STAGE + "/connect/authorize";

        /// <summary>
        /// Use this to get access token/request token.
        /// </summary>
        public const string ENDPOINT_GET_ACCESS_TOKEN       = ENDPOINT_STAGE + "/connect/token";

        /// <summary>
        /// Use this to refresh access token.
        /// </summary>
        public const string ENDPOINT_REFRESH_ACCESS_TOKEN   = ENDPOINT_STAGE + "/connect/token";

        /// <summary>
        /// Use this to revoke tokens for a user.
        /// </summary>
        public const string ENDPOINT_REVOKE_ACCESS_TOKEN    = ENDPOINT_STAGE + "/connect/revocation";

        public const string SCOPE_bloodpressure     = "bloodpressure";
        public const string SCOPE_activity          = "activity";
        public const string SCOPE_openid            = "openid";
        public const string SCOPE_offline_access    = "offline_access";

        public const string GRANT_TYPE_AUTHORIZATION_CODE = "authorization_code";
        public const string GRANT_TYPE_REFRESH_TOKEN = "refresh_token";

        public const string RESPONSE_TYPE_CODE = "code";

        private string _ClientId;
        private string _ClientSecret;
        private Uri _RedirectUrl;

        public OAuthV2Authenticate(string clientId, string clientSecret, string redirectUrl)
        {
            _ClientId = clientId;
            _ClientSecret = clientSecret;
            _RedirectUrl = new Uri(redirectUrl);
        }

        public String GetAuthorizationLink()
        {
            // TO DO : Add validations

            Uri uriAuth = new Uri(ENDPOINT_AUTHORIZATION);

            uriAuth = uriAuth.AddUriParameter("response_type", RESPONSE_TYPE_CODE);
            uriAuth = uriAuth.AddUriParameter("client_id", _ClientId);
            uriAuth = uriAuth.AddUriParameter("state", Guid.NewGuid().ToString("N"));
            uriAuth = uriAuth.AddUriParameter("scope", String.Join(" ", SCOPE_offline_access, SCOPE_openid, SCOPE_bloodpressure, SCOPE_activity));
            uriAuth = uriAuth.AddUriParameter("redirect_uri", _RedirectUrl.ToString());

            return uriAuth.ToString();
        }

        public OAuthV2ResponseObject ManageUserAuthCallback()
        {
            if (HttpContext.Current == null)
                throw new Exception("No web context");
            if (HttpContext.Current.Request["code"] == null)
                throw new Exception("No access code returned");

            // Read access code provided by Omron. 
            OAuthV2SessionManager.AccessCode = HttpContext.Current.Request["code"];

            // Use this code to request Access token.
            Uri uriGetAccesToken = new Uri(ENDPOINT_GET_ACCESS_TOKEN);
            StringBuilder sb = new StringBuilder();

            sb.Append("scope=" + String.Join(" ", SCOPE_offline_access, SCOPE_openid, SCOPE_bloodpressure, SCOPE_activity));
            sb.Append("&grant_type=" + GRANT_TYPE_AUTHORIZATION_CODE);
            sb.Append("&client_id=" + _ClientId);
            sb.Append("&client_secret=" + _ClientSecret);
            sb.Append("&code=" + OAuthV2SessionManager.AccessCode);
            sb.Append("&redirect_uri=" + _RedirectUrl.ToString());

            var response = OAuthV2RequestHelper.SendRequest(uriGetAccesToken, sb.ToString());

            // Populate Session variables
            OAuthV2SessionManager.AccessToken = response.AccessToken;
            OAuthV2SessionManager.RefreshToken = response.RefreshToken;
            OAuthV2SessionManager.UserId = response.UserId;

            return response;
        }

        public OAuthV2ResponseObject RefreshAccessToken()
        {
            if (!OAuthV2SessionManager.IsAccessTokenSet())
                throw new Exception("No access token set.");
            if (!OAuthV2SessionManager.IsRefreshTokenSet())
                throw new Exception("No refresh token set.");

            // Refresh Access Token
            Uri uriGetAccesToken = new Uri(ENDPOINT_REFRESH_ACCESS_TOKEN);
            StringBuilder sb = new StringBuilder();

            sb.Append("grant_type=" + GRANT_TYPE_REFRESH_TOKEN);
            sb.Append("&client_id=" + _ClientId);
            sb.Append("&client_secret=" + _ClientSecret);
            sb.Append("&redirect_uri=" + _RedirectUrl);
            sb.Append("&refresh_token=" + OAuthV2SessionManager.RefreshToken);

            var response = OAuthV2RequestHelper.SendRequest(uriGetAccesToken, sb.ToString());

            // Populate Session variables
            OAuthV2SessionManager.AccessToken = response.AccessToken;
            OAuthV2SessionManager.RefreshToken = response.RefreshToken;

            return response;
        }

    }
}