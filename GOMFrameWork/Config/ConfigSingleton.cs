using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace GOMFrameWork.Config
{
    internal class ConfigSingleton
    {
        private static object _object = new object();
        private static ConfigSingleton _instance = null;

        public GOMFrameWork Configs { get; private set; }

        private ConfigSingleton()
        {
            Configs = ConfigurationManager.GetSection("GOMFrameWork") as GOMFrameWork;
        }

        public static ConfigSingleton Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_object)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigSingleton();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
