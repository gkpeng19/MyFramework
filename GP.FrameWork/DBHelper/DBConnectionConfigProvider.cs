using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GP.FrameWork.DBHelper
{
    /// <summary>
    /// 配置文件中节点，如：<dbConnectionGroup><dbConnections><connection key="" dbtype="" /></dbConnections></dbConnectionGroup>
    /// </summary>
    internal class DBConnectionConfigProvider : ConfigurationSection
    {
        [ConfigurationProperty("DBConnectionCollection", IsDefaultCollection = false)]
        public DBConnectionCollection Collection
        {
            get
            {
                return this["DBConnectionCollection"] as DBConnectionCollection;
            }
        }
    }

    internal sealed class DBConnection : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true)]
        public string Key
        {
            get { return this["key"] as string; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("dbtype")]
        public string DbType
        {
            get { return this["dbtype"] as string; }
            set { this["dbtype"] = value; }
        }

        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[Key].ConnectionString;
        }
    }


    internal sealed class DBConnectionCollection : ConfigurationElementCollection
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
}
