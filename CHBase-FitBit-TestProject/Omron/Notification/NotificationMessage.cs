using CHBase.Omron.Omron.OAuthV2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CHBase.Omron.Omron.Notification
{
    public class NotificationMessage
    {
        /// <summary>
        /// UserId parsed from RawResponse which will be the identifier of the User who performed the upload.
        /// This will match the value for the user you received in the id_token when your application first 
        /// received authorization from the user.
        /// </summary>
        /// <remarks>
        /// Should be a Guid in most cases. TODO: verfiy if this is always the case.
        /// </remarks>
        public string UserId { get; set; }

        /// <summary>
        /// Timestamp parsed from RawResponse which will be the 
        /// UTC date/time the upload occurred formatted in ISO 8601 format.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The raw response string
        /// </summary>
        public string RawMessageBody { get; set; }

        public NotificationMessage(string rawMsgBody)
        {
            RawMessageBody = rawMsgBody;

            var dictJsonResponse = JsonConvert.DeserializeObject<Dictionary<String, String>>(RawMessageBody);
            UserId = dictJsonResponse["id"];
            TimeStamp = DateTime.Parse(dictJsonResponse["timestamp"], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }


        public void Handle()
        {
            // Store object to file
            string folder_path = ConfigurationManager.AppSettings["NotificationSaveFolder"];

            // Verify that it is a folder.
            FileAttributes folder_attr = File.GetAttributes(folder_path);
            if (!folder_attr.HasFlag(FileAttributes.Directory))
                throw new Exception("NotificationSaveFolder should be a directory.");

            string file_name = DateTime.UtcNow.ToEpochTime().ToString() + ".txt";
            string final_file_path = Path.Combine(folder_path, file_name);

            using (StreamWriter oFile = new StreamWriter(final_file_path, false, Encoding.UTF8))
            {
                oFile.Write(String.Format("Notification received at : {0} (IST). \r\n\r\nData provided below. \r\n\r\n", DateTime.Now));
                oFile.Write(this.ToString());
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("********************************************* \r\n\r\n");

            foreach (var property in this.GetType().GetProperties())
            {
                sb.AppendFormat("{0}={1} \r\n\r\n", property.Name, property.GetValue(this));
            }

            sb.Append("********************************************* \r\n\r\n");

            return sb.ToString();
        }
    }
}