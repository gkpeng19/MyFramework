using G.Util.Tool;
using GOMFrameWork.DataEntity;
using GOMFrameWork.Utils;
using Newtonsoft.Json.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Redis
{
    internal interface IRedisRepository
    {
        bool Exist<TEntity>() where TEntity : EntityBase;
        List<TEntity> GetAll<TEntity>() where TEntity : EntityBase, new();
        TEntity Find<TEntity>(long entityId) where TEntity : EntityBase, new();
        void Save(EntityBase entity);
        void Delete<TEntity>(EntityBase entity);
    }

    public class RedisRepository
    {
        static object _object = new object();
        static RedisRepository _repository = null;
        static string hashname = "hashname";

        private RedisRepository() { }

        public static RedisRepository Default
        {
            get
            {
                if (_repository == null)
                {
                    lock (_object)
                    {
                        if (_repository == null)
                        {
                            _repository = new RedisRepository();
                        }
                    }
                }
                return _repository;
            }
        }

        public void Set(string key, string value)
        {
            if (key == null || key.Length == 0 || value == null)
            {
                return;
            }
            RedisManager.GetMaster().HSet(hashname, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value));
        }

        public void Set<T>(string key, T entity) where T : EntityBase
        {
            if (key == null || key.Length == 0 || entity == null)
            {
                return;
            }
            var jEntity = JSON.GetJObject(JSON.GetJProperty(entity.PrimaryKey, entity.ID));
            foreach (EntityItem item in entity.Collection.Values)
            {
                jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
            }
            RedisManager.GetMaster().HSet(hashname, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(jEntity.ToString()));
        }

        public void SetList<T>(string key, List<T> list) where T : EntityBase
        {
            if (key == null || key.Length == 0 || list == null || list.Count == 0)
            {
                return;
            }

            JArray collection = new JArray();
            list.ForEach(e =>
                {
                    var jEntity = JSON.GetJObject(JSON.GetJProperty(e.PrimaryKey, e.ID));
                    foreach (EntityItem item in e.Collection.Values)
                    {
                        jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
                    }
                    collection.Add(jEntity);
                });
            RedisManager.GetMaster().HSet(hashname, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(collection.ToString()));
        }

        public string Get(string key)
        {
            if (key == null || key.Length == 0)
            {
                return null;
            }

            var bytes = RedisManager.GetClient().HGet(hashname, Encoding.UTF8.GetBytes(key));
            if (bytes != null && bytes.Length > 0)
            {
                return Encoding.UTF8.GetString(bytes);
            }
            return null;
        }

        public T Get<T>(string key) where T : EntityBase, new()
        {
            if (key == null || key.Length == 0)
            {
                return null;
            }

            var bytes = RedisManager.GetClient().HGet(hashname, Encoding.UTF8.GetBytes(key));
            if (bytes != null && bytes.Length > 0)
            {
                T target = new T();
                var jobject = JSON.GetJObject(Encoding.UTF8.GetString(bytes));
                foreach (JProperty pi in jobject.Properties())
                {
                    target.Collection[pi.Name] = new EntityItem() { Key = pi.Name, Value = pi.Value.ToString() };
                }
                return target;
            }
            return null;
        }

        public List<T> GetList<T>(string key) where T : EntityBase, new()
        {
            List<T> list = new List<T>();

            if (key == null || key.Length == 0)
            {
                return list;
            }

            var bytes = RedisManager.GetClient().HGet(hashname, Encoding.UTF8.GetBytes(key));
            if (bytes != null && bytes.Length > 0)
            {
                var jarray = JSON.GetJArray(Encoding.UTF8.GetString(bytes));
                foreach (JObject jobject in jarray)
                {
                    T target = new T();
                    foreach (JProperty pi in jobject.Properties())
                    {
                        target.Collection[pi.Name] = new EntityItem() { Key = pi.Name, Value = pi.Value.ToString() };
                    }
                    list.Add(target);
                }
            }
            return list;
        }
    }
}
