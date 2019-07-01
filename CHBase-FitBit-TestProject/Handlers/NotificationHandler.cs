using System;
using System.Net;
using System.Web;

namespace CHBase_FitBit_TestProject.Handlers
{
    public class NotificationHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

       

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse r = context.Response;
            string VerificationCode = context.Request.QueryString["verify"];
            if (!String.IsNullOrEmpty(VerificationCode))
            {
                if (VerificationCode == "9cd16f797f4195a10178693fd665bf8a822fbe6edef8fad3974f2d2f2c6ec7a5")
                    r.StatusCode = (int)HttpStatusCode.NoContent;

                else
                    r.StatusCode = (int)HttpStatusCode.NotFound;

            }
        }

        #endregion
    }
}
