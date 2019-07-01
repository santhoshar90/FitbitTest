using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace CHBase.Omron.Omron.Pages
{

    public static class DBHelper
    {
        /// <summary>
        /// Serializes data to a file.
        /// </summary>
        public static void Create(OAuthV2.OAuthV2ResponseObject ResponseObject)
        {
            string file_path = @"Z:\GIT\CHBase-FitBit-TestProject\CHBase-FitBit-TestProject\Files\TEst.txt";//ConfigurationManager.AppSettings["DataFilePath"];
            if (String.IsNullOrEmpty(file_path))
                throw new Exception("DatFilePath not provided in config.");

            if (ResponseObject == null) { throw new Exception("No data to store to file"); }

          
            using (Stream stream = File.Open(file_path, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, ResponseObject);
            }
        }

        public static OAuthV2.OAuthV2ResponseObject Get()
        {
            string file_path = @"Z:\GIT\CHBase-FitBit-TestProject\CHBase-FitBit-TestProject\Files\TEst.txt";//ConfigurationManager.AppSettings["DataFilePath"];
            if (String.IsNullOrEmpty(file_path))
                throw new Exception("DatFilePath not provided in config.");

            using (Stream stream = File.Open(file_path, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (OAuthV2.OAuthV2ResponseObject)binaryFormatter.Deserialize(stream);
            }
        }

    }
}