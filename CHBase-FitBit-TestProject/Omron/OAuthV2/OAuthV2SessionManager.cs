using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHBase.Omron.Omron.OAuthV2
{
    public class OAuthV2SessionManager
    {
        public static bool IsAccessCodeSet()
        {
            return !String.IsNullOrEmpty(AccessCode);
        }

        public static string AccessCode
        {
            get
            {
                if (!_IsWebContext()) return null;

                return _Get("AccessCode");
            }

            set
            {
                if (!_IsWebContext()) return;

                _Set("AccessCode", value);
            }
        }


        public static bool IsAccessTokenSet()
        {
            return !String.IsNullOrEmpty(AccessToken);
        }

        public static string AccessToken
        {
            get
            {
                if (!_IsWebContext()) return null;

                return _Get("AccessToken");
            }

            set
            {
                if (!_IsWebContext()) return;

                _Set("AccessToken", value);
            }
        }

        public static bool IsRefreshTokenSet()
        {
            return !String.IsNullOrEmpty(RefreshToken);
        }

        public static string RefreshToken
        {
            get
            {
                if (!_IsWebContext()) return null;

                return _Get("RefreshToken");
            }

            set
            {
                if (!_IsWebContext()) return;

                _Set("RefreshToken", value);
            }
        }

        public static bool IsUserIdSet()
        {
            return !String.IsNullOrEmpty(UserId);
        }

        public static string UserId
        {
            get
            {
                if (!_IsWebContext()) return null;

                return _Get("UserId");
            }

            set
            {
                if (!_IsWebContext()) return;

                _Set("UserId", value);
            }
        }

        static bool _IsWebContext()
        {
            return HttpContext.Current != null;
        }

        static void _Set(string key, string value)
        {
            if (!_IsWebContext()) return;

            HttpContext.Current.Session[key] = value;
        }

        static string _Get(string key)
        {
            if (!_IsWebContext()) return null;

            if (HttpContext.Current.Session[key] == null) return null;

            return (string)HttpContext.Current.Session[key];
        }
    }
}