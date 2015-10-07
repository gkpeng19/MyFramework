using EntityLibrary;
using G.Util.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebSite
{
    public static class CommonUtil
    {
        public static string SystemID
        {
            get
            {
                var systemid = ConfigurationManager.AppSettings["SystemID"];
                if (systemid == null)
                {
                    return string.Empty;
                }
                return systemid;
            }
        }
    }
}