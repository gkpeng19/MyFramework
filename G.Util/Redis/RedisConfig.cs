using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Redis
{
    internal class RedisSingleton
    {
        private static object _object = new object();
        private static RedisSingleton _redis = null;

        internal RedisService RedisService { get; private set; }

        private RedisSingleton()
        {
            try
            {
                RedisService = ConfigurationManager.GetSection("RedisService") as RedisService;
            }
            catch (Exception ex)
            {
                throw new Exception("Redis配置错误：" + ex.Message);
            }
        }

        internal static RedisSingleton Current
        {
            get
            {
                if (_redis == null)
                {
                    lock (_object)
                    {
                        if (_redis == null)
                        {
                            _redis = new RedisSingleton();
                        }
                    }
                }
                return _redis;
            }
        }
    }

    internal class RedisService : ConfigurationSection
    {
        [ConfigurationProperty("RedisMaster")]
        public RedisMaster Master
        {
            get
            {
                return this["RedisMaster"] as RedisMaster;
            }
        }

        [ConfigurationProperty("RedisSelves")]
        public RedisSelves Selves
        {
            get
            {
                return this["RedisSelves"] as RedisSelves;
            }
        }
    }

    internal class RedisMachine : ConfigurationElement
    {
        [ConfigurationProperty("ip")]
        public string Ip
        {
            get { return this["ip"] as string; }
            set { this["ip"] = value; }
        }
        [ConfigurationProperty("port")]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }
    }

    internal class RedisMaster : RedisMachine
    {
    }

    internal class RedisSelve : RedisMachine
    {
        [ConfigurationProperty("name", IsKey = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
    }

    internal class RedisSelves : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RedisSelve();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((RedisSelve)element).Name;
        }

        protected override string ElementName
        {
            get { return "RedisSelve"; }
        }

        public new int Count
        {
            get { return base.Count; }
        }


        public RedisSelve this[int index]
        {
            get
            {
                return (RedisSelve)BaseGet(index);
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

        new public RedisSelve this[string Name]
        {
            get
            {
                return (RedisSelve)BaseGet(Name);
            }
        }

        public int IndexOf(RedisSelve element)
        {
            return BaseIndexOf(element);
        }

        public void Add(RedisSelve element)
        {
            BaseAdd(element);
        }

        public void Remove(RedisSelve element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
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
