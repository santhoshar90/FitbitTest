using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CHBase.Omron.Omron.Notification
{
    /// <summary>
    /// Summary description for NotificationHandler
    /// </summary>
    public class NotificationHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                HandleRequest(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400;
                throw new HttpRequestValidationException("Invalid data provided", ex);
            }
        }

        private static void HandleRequest(HttpContext context)
        {
            string rawRequest;
            var reqStream = context.Request.InputStream;
            using (var reqStreamReader = new StreamReader(reqStream, Encoding.ASCII))
            {
                rawRequest = reqStreamReader.ReadToEnd();
            }

            var r = new NotificationMessage(rawRequest);
            r.Handle();
            context.Response.StatusCode = 200;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}