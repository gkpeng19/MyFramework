using GP.FrameWork.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace GP.FrameWork.DBHelper
{
    internal class DBConnectionManager
    {
        private static object _object = new object();
        private static DBConnectionManager _instance = null;

        public DBConnectionConfigProvider ConnectionConfig { get; private set; }

        private DBConnectionManager()
        {
            ConnectionConfig = ConfigurationManager.GetSection("DBConnectionConfigProvider") as DBConnectionConfigProvider;
        }

        public static DBConnectionManager Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (_object)
                    {
                        if (_instance == null)
                        {
                            _instance = new DBConnectionManager();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
