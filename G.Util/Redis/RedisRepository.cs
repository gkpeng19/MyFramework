using G.Util.Tool;
using GOMFrameWork.DataEntity;
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
        TEntity Find<TEntity>(EntityBase entity) where TEntity : EntityBase, new();
        void Save(EntityBase entity);
        void Delete(EntityBase entity);
    }

    public class RedisRepository : IRedisRepository
    {
        static object _object = new object();
        static RedisRepository _repository = null;

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

        private bool Exist(string typeName)
        {
            if (typeName != null && typeName.Length > 0)
            {
                return RedisManager.GetClient().Exists(typeName) == 0 ? false : true;
            }
            return false;
        }

        protected internal virtual bool Exist(EntityBase entity)
        {
            if (entity.ID > 0)
            {
                var entityIds = RedisManager.GetClient().GetAllItemsFromList(entity.GetType().Name);
                if (entityIds != null && entityIds.Contains(entity.ID.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Exist<TEntity>() where TEntity : EntityBase
        {
            return Exist(typeof(TEntity).Name);
        }

        public virtual List<TEntity> GetAll<TEntity>() where TEntity : EntityBase, new()
        {
            List<TEntity> list = new List<TEntity>();

            var _listId = typeof(TEntity).Name;
            var client = RedisManager.GetClient();

            var entityIds = client.GetAllItemsFromList(_listId);
            if (entityIds == null)
            {
                return list;
            }

            foreach (var pid in entityIds)
            {
                byte[] bytes = client.Get(_listId + pid);
                if (bytes == null || bytes.Length == 0)
                {
                    continue;
                }
                var jEntity = JSON.GetJObject(Encoding.UTF8.GetString(bytes));
                TEntity target = new TEntity();
                foreach (Newtonsoft.Json.Linq.JProperty pi in jEntity.Properties())
                {
                    target.Collection[pi.Name] = new EntityItem() { Key = pi.Name, Value = pi.Value.ToString() };
                }
                list.Add(target);
            }
            return list;
        }

        public virtual TEntity Find<TEntity>(EntityBase entity) where TEntity : EntityBase, new()
        {
            if (entity.ID > 0)
            {
                var _listId = entity.GetType().Name;
                var _id = entity.ID.ToString();
                var client = RedisManager.GetClient();

                var entityIds = client.GetAllItemsFromList(_listId);
                if (entityIds != null && entityIds.Contains(_id))
                {
                    var bytes = client.Get(_listId + _id);
                    if (bytes == null || bytes.Length == 0)
                    {
                        return null;
                    }

                    var jEntity = JSON.GetJObject(Encoding.UTF8.GetString(bytes));
                    TEntity target = new TEntity();
                    foreach (Newtonsoft.Json.Linq.JProperty pi in jEntity.Properties())
                    {
                        target.Collection[pi.Name] = new EntityItem() { Key = pi.Name, Value = pi.Value.ToString() };
                    }

                    return target;
                }
            }
            return null;
        }

        public virtual void Save(EntityBase entity)
        {
            if (entity != null)
            {
                string pid = entity.ID.ToString();
                string _listId = entity.GetType().Name;
                var master = RedisManager.GetMaster();

                if (Exist(entity))
                {
                    var ek = _listId + pid;
                    var bytes = master.Get(ek);
                    if (bytes != null && bytes.Length > 0)
                    {
                        var jEntity = JSON.GetJObject(Encoding.UTF8.GetString(bytes));
                        foreach (EntityItem item in entity.Collection.Values)
                        {
                            jEntity.Remove(item.Key);
                            jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
                        }

                        master.Remove(_listId + pid);
                        master.Set(_listId + pid, Encoding.UTF8.GetBytes(jEntity.ToString()));
                    }
                }
                else
                {
                    var jEntity = JSON.GetJObject(JSON.GetJProperty(entity.PrimaryKey, entity.ID));
                    foreach (EntityItem item in entity.Collection.Values)
                    {
                        jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
                    }
                    master.Set(_listId + pid, Encoding.UTF8.GetBytes(jEntity.ToString()));
                    master.AddItemToList(_listId, pid);
                }
                master.BgSave();
            }
        }

        public virtual void Delete(EntityBase entity)
        {
            if (entity != null)
            {
                var master = RedisManager.GetMaster();
                var _listId = entity.GetType().Name;
                master.Remove(_listId + entity.ID.ToString());
                master.RemoveItemFromList(_listId, entity.ID.ToString());
                master.BgSave();
            }
        }
    }
}
