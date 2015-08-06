using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace GP.FrameWork.DBHelper
{
    /// <summary>
    /// 配置文件中节点，如：<dbConnectionGroup><dbConnections><connection key="" dbtype="" /></dbConnections></dbConnectionGroup>
    /// </summary>
    public class DBConnectionConfigProvider : ConfigurationSection
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

    public sealed class DBConnection : ConfigurationElement
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

    public class DBConnectionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new DBConnection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as DBConnection).Key;
        }

        public DBConnection this[string key]
        {
            get { return BaseGet(key) as DBConnection; }
        }

        public DBConnection this[int index]
        {
            get { return BaseGet(0) as DBConnection; }
        }

        protected override string ElementName
        {
            get
            {
                return "DBConnection";
            }
        }
    }


}
