using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CHBase_FitBit_TestProject.Helpers
{
    public class Utils
    {
        public static string GetQueryResponse(string strQuery)
        {
            var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strQuery);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                return result;
            }
        }
    }
}