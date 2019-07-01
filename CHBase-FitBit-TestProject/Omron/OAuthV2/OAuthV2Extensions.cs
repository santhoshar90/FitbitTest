using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHBase.Omron.Omron.OAuthV2
{
    internal static class OAuthV2Extensions
    {
        public static Uri AddUriParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        public static int ToEpochTime(this DateTime dt)
        {
            DateTime Epoch_Base = new DateTime(1970, 1, 1);
            return (int)(dt - Epoch_Base).TotalSeconds;
        }
    }
}