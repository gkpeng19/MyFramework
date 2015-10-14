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
        List<TEntity> GetAll<TEntity>() where TEntity : RedisEntity, new();
        TEntity Find<TEntity>(RedisEntity entity) where TEntity : RedisEntity, new();
        void Save(RedisEntity entity);
        void Delete(RedisEntity entity);
    }

    public class RedisRepository : IRedisRepository
    {
        private RedisClient _redis = null;
        public RedisRepository()
        {
            _redis = RedisManager.GetClient();
        }

        protected internal virtual bool IsExist(RedisEntity entity)
        {
            if (entity.ID > 0)
            {
                var entityIds = _redis.GetAllItemsFromList(entity.GetType().Name);
                if (entityIds.Contains(entity.ID.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual List<TEntity> GetAll<TEntity>() where TEntity : RedisEntity, new()
        {
            List<TEntity> list = new List<TEntity>();
            var _listId = typeof(TEntity).Name;
            foreach (var pid in _redis.GetAllItemsFromList(_listId))
            {
                byte[] bytes = _redis.Get(_listId + pid);
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

        public virtual TEntity Find<TEntity>(RedisEntity entity) where TEntity : RedisEntity, new()
        {
            if (entity.ID > 0)
            {
                var _listId = entity.GetType().Name;
                var _id = entity.ID.ToString();

                var entityIds = _redis.GetAllItemsFromList(_listId);
                if (entityIds.Contains(_id))
                {
                    var bytes = _redis.Get(_listId + _id);
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

        public virtual void Save(RedisEntity entity)
        {
            if (entity != null)
            {
                string pid = entity.ID.ToString();
                string _listId = entity.GetType().Name;

                if (IsExist(entity))
                {
                    var ek = _listId + pid;
                    var bytes = _redis.Get(ek);
                    if (bytes != null && bytes.Length > 0)
                    {
                        var jEntity = JSON.GetJObject(Encoding.UTF8.GetString(bytes));
                        foreach (EntityItem item in entity.Collection.Values)
                        {
                            jEntity.Remove(item.Key);
                            jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
                        }

                        _redis.Remove(_listId + pid);
                        _redis.Set(_listId + pid, Encoding.UTF8.GetBytes(jEntity.ToString()));
                    }
                }
                else
                {
                    var jEntity = JSON.GetJObject(JSON.GetJProperty(entity.PrimaryKey, entity.ID));
                    foreach (EntityItem item in entity.Collection.Values)
                    {
                        jEntity.Add(JSON.GetJProperty(item.Key, item.Value));
                    }
                    _redis.Set(_listId + pid, Encoding.UTF8.GetBytes(jEntity.ToString()));
                    _redis.AddItemToList(_listId, pid);
                }
                _redis.BgSave();
            }
        }

        public virtual void Delete(RedisEntity entity)
        {
            if (entity != null)
            {
                var _listId = entity.GetType().Name;
                _redis.Remove(_listId + entity.ID.ToString());
                _redis.RemoveItemFromList(_listId, entity.ID.ToString());
                _redis.BgSave();
            }
        }
    }

    public class RedisEntity : EntityBase
    {
        public RedisEntity() : base() { }

        public override long Save()
        {
            EntityBase e = this;
            var id = base.Save();
            if (id > 0)
            {
                //更新缓存
                RedisRepository re = new RedisRepository();
                re.Save(this);
            }
            return id;
        }

        public override long Delete()
        {
            var r = base.Delete();
            if (r > 0)
            {
                //更新缓存
                RedisRepository re = new RedisRepository();
                re.Delete(this);
            }
            return r;
        }
    }
}
