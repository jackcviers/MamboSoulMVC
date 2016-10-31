using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CommonUtilities
{
    public static class FileManager
    {
        public static string GetFileExtension(string imageUrl)
        {
            string result = null;
            string[] extensions = { "jpg", "png" };
            for (int i = 0; i < extensions.Length; i++)
            {
                result = imageUrl + extensions[i];
                if (File.Exists(result))
                {
                    return result;
                }
                else result = null;
            }
            
            return result;
        }

        private static bool fileExists(string imageUrl)
        {
            bool exists = false;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(imageUrl);
            request.Method = "HEAD";
            try
            {
                request.GetResponse();
                exists = true;
            }
            catch
            {
                exists = false;
            }

            return exists;
        }
    }
}