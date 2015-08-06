using GOMFrameWork.Config;
using GOMFrameWork.DataEntity;
using GOMFrameWork.DBContext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOMFrameWork
{
    public static class DbContext
    {
        static Hashtable source = null;
        static DbContext()
        {
            source = new Hashtable(StringComparer.OrdinalIgnoreCase);
            var mappings = ConfigSingleton.Current.Configs.EPMappings;
            if (mappings != null && mappings.Count > 0)
            {
                foreach (EPMapping map in mappings)
                {
                    if (!source.ContainsKey(map.EntityType))
                    {
                        source.Add(map.EntityType, map.ConnectionKey);
                    }
                }
            }
        }

        public static void InitContext<T>(string key) where T : EntityBase
        {
            var typeName = typeof(T).FullName;
            if (!source.ContainsKey(typeName))
            {
                source.Add(typeName, key);
            }
        }

        static EntityProvider GetEntityProvider(Type entityType)
        {
            if (entityType == typeof(SearchEntity))
            {
                return EntityProviderFactory.GetProvider(source["SearchEntity"].ToString());
            }
            else if (entityType == typeof(ProcEntity))
            {
                return EntityProviderFactory.GetProvider(source["ProcEntity"].ToString());
            }

            while (!source.ContainsKey(entityType.FullName))
            {
                entityType = entityType.BaseType;
                if (entityType == typeof(EntityBase))
                {
                    return EntityProviderFactory.GetProvider(source["EntityBase"].ToString());
                }
                else if (entityType == typeof(SearchEntity))
                {
                    return EntityProviderFactory.GetProvider(source["SearchEntity"].ToString());
                }
                else if (entityType == typeof(ProcEntity))
                {
                    return EntityProviderFactory.GetProvider(source["ProcEntity"].ToString());
                }

                if (entityType == typeof(object))
                {
                    throw new Exception("No Found EntityProvider, Please Check Web Config!");
                }
            }
            return EntityProviderFactory.GetProvider(source[entityType.FullName].ToString());
        }

        internal static EntityProvider GetEntityProvider(EntityProvider provider, Type entityType, ref bool isNew)
        {
            //对于非Save方法，provider恒等于Null
            if (provider == null)
            {
                return GetEntityProvider(entityType);
            }

            //用的下面代码的只有Save方法，实体只可能是EntityBase
            string key = entityType.FullName;
            while (!source.ContainsKey(key))
            {
                entityType = entityType.BaseType;
                if (entityType == typeof(EntityBase))
                {
                    key = "EntityBase";
                    break;
                }
                else if (entityType == typeof(object))
                {
                    throw new Exception("No Found EntityProvider, Please Check Web Config!");
                }

                key = entityType.FullName;
            }

            if (source[key].ToString().Equals(provider.ContextKey))
            {
                isNew = false;
                return provider;
            }

            return EntityProviderFactory.GetProvider(key);
        }
    }
}
