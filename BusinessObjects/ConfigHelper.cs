using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public static class ConfigHelper
    {
        public const string DEFAULTCONFIG = "default";

        public static string ConnectionString()
        {
            string result = string.Empty;

            try
            {
                result = GetMyConnectionString();

                if (result == null || result == string.Empty)
                    throw new Exception();
            }
            catch(Exception e)
            {
                throw new Exception("AppSettings are empty: " + e.ToString());
            }

            return result;
        }

        private static string GetMyConnectionString()
        {
            string result = string.Empty;

            string[] keys = { Environment.MachineName.ToLower(), DEFAULTCONFIG };

            foreach (string attr in keys)
            {
                try
                {
                    result = ConfigurationManager.ConnectionStrings[attr].ConnectionString;
                    break;
                }
                catch (Exception e)
                {
                    // Don't do anything, we didn't get anything back
                }
            }

            return result;
        }
    }
}