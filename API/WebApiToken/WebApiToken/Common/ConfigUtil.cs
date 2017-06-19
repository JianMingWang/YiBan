using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApiToken.Common
{
    public class ConfigUtil
    {
        public static string GetAppSetValue(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
                return "";
            try
            {
                return Convert.ToString(ConfigurationManager.AppSettings[appKey]);
            }
            catch
            {
                return "";
            }
        }
    }
}