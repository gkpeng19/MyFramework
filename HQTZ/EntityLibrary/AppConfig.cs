using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLibrary
{
    public static class AppConfig
    {
        static AppConfig()
        {
            DaysBeforePay = int.Parse(ConfigurationManager.AppSettings["DaysBeforePay"]);
        }

        public static int DaysBeforePay { get; private set; }
    }
}
