using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GOMFrameWork.Config
{
    /// <summary>
    /// 数据库连接配置节点
    /// </summary>
    internal class GOMFrameWork : ConfigurationSection
    {
        [ConfigurationProperty("DBConnections", IsDefaultCollection = false)]
        public DBConnections DBConnections
        {
            get
            {
                return this["DBConnections"] as DBConnections;
            }
        }

        [ConfigurationProperty("EPMappings", IsDefaultCollection = false)]
        public EPMappings EPMappings
        {
            get
            {
                return this["EPMappings"] as EPMappings;
            }
        }
    }

    /// <summary>
    /// 数据库连接配置
    /// </summary>
    internal sealed class DBConnection : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true)]
        public string Key
        {
            get { return this["key"] as string; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("dbType")]
        public string DbType
        {
            get { return this["dbType"] as string; }
            set { this["dbType"] = value; }
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[Key].ConnectionString;
        }
    }

    /// <summary>
    /// 数据库连接配置集合
    /// </summary>
    internal sealed class DBConnections : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DBConnection();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DBConnection)element).Key;
        }

        protected override string ElementName
        {
            get { return "DBConnection"; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public DBConnection this[int index]
        {
            get
            {
                return (DBConnection)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public DBConnection this[string Name]
        {
            get
            {
                return (DBConnection)BaseGet(Name);
            }
        }

        public int IndexOf(DBConnection element)
        {
            return BaseIndexOf(element);
        }

        public void Add(DBConnection element)
        {
            BaseAdd(element);
        }

        public void Remove(DBConnection element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Key);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string client)
        {
            BaseRemove(client);
        }

        public void Clear()
        {
            BaseClear();
        }
    }

    /// <summary>
    /// Entity与EntityProvider映射
    /// </summary>
    internal sealed class EPMapping : ConfigurationElement
    {
        [ConfigurationProperty("entityType", IsKey = true)]
        public string EntityType
        {
            get { return this["entityType"] as string; }
            set { this["entityType"] = value; }
        }

        [ConfigurationProperty("connectionKey")]
        public string ConnectionKey
        {
            get { return this["connectionKey"] as string; }
            set { this["connectionKey"] = value; }
        }
    }

    /// <summary>
    /// Entity与EntityProvider映射集合
    /// </summary>
    internal sealed class EPMappings : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EPMapping();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((EPMapping)element).EntityType;
        }

        protected override string ElementName
        {
            get { return "EPMapping"; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public EPMapping this[int index]
        {
            get
            {
                return (EPMapping)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public EPMapping this[string Name]
        {
            get
            {
                return (EPMapping)BaseGet(Name);
            }
        }

        public int IndexOf(EPMapping element)
        {
            return BaseIndexOf(element);
        }

        public void Add(EPMapping element)
        {
            BaseAdd(element);
        }

        public void Remove(EPMapping element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.EntityType);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string client)
        {
            BaseRemove(client);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
