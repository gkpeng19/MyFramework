using GOMFrameWork.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GOMFrameWork.DBContext
{
    internal class EntityProviderFactory
    {
        static object _obj = null;
        static Dictionary<string, Queue<EntityProvider>> EPDic = null;

        static EntityProviderFactory()
        {
            _obj = new object();
            EPDic = new Dictionary<string, Queue<EntityProvider>>();
        }

        public static EntityProvider GetProvider(string key)
        {
            var dbconfig = ConfigSingleton.Current.Configs.DBConnections[key];

            EntityProvider provider = null;
            if (!EPDic.ContainsKey(key))
            {
                EPDic[key] = new Queue<EntityProvider>();
                provider = CreateNewProvider(dbconfig);
                provider.OnCompleted += provider_OnCompleted;
                return provider;
            }

            var queue = EPDic[key];
            if (queue.Count > 0)
            {
                lock (_obj)
                {
                    if (queue.Count > 0)
                    {
                        provider = queue.Dequeue();
                    }
                }
            }

            provider = CreateNewProvider(dbconfig);
            provider.OnCompleted += provider_OnCompleted;
            return provider;
        }

        static EntityProvider CreateNewProvider(DBConnection dbconfig)
        {
            if (dbconfig.DbType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
            {
                return new SqlEntityProvider(dbconfig.Key, dbconfig.GetConnectionString());
            }
            else if (dbconfig.DbType.Equals("oracle", StringComparison.OrdinalIgnoreCase))
            {
                return new OracleEntityProvider(dbconfig.Key, dbconfig.GetConnectionString());
            }
            else
            {
                throw new Exception("error config section：the section dbConnectionGroup in dbtype property!");
            }
        }

        static void provider_OnCompleted(string key, EntityProvider provider)
        {
            lock (_obj)
            {
                EPDic[key].Enqueue(provider);
            }
        }
    }
}