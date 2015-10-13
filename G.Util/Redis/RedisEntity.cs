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
        List<TEntity> GetAll<TEntity>() where TEntity : EntityBase;
        TEntity Find<TEntity>(long id) where TEntity : EntityBase;
        void Save<TEntity>(TEntity entity) where TEntity : EntityBase;
        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;
    }

    public class RedisRepository : IRedisRepository
    {
        private RedisClient _redis = null;
        public RedisRepository()
        {
            _redis = RedisManager.GetClient();
        }

        protected internal virtual bool IsExist<TEntity>(long id) where TEntity : EntityBase
        {
            if (id > 0)
            {
                var entityIds = _redis.GetAllItemsFromList(typeof(TEntity).Name);
                if (entityIds.Contains(id.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual List<TEntity> GetAll<TEntity>() where TEntity : EntityBase
        {
            List<TEntity> list = new List<TEntity>();
            foreach (var pid in _redis.GetAllItemsFromList(typeof(TEntity).Name))
            {
                byte[] bytes = _redis.Get(pid);
                if (bytes == null || bytes.Length == 0)
                {
                    continue;
                }
                list.Add(JSON.JsonBack<TEntity>(Encoding.UTF8.GetString(bytes)));
            }
            return list;
        }

        public virtual TEntity Find<TEntity>(long id) where TEntity : EntityBase
        {
            if (id > 0)
            {
                var _listId = typeof(TEntity).Name;

                var entityIds = _redis.GetAllItemsFromList(_listId);
                if (entityIds.Contains(id.ToString()))
                {
                    var bytes = _redis.Get(_listId + id.ToString());
                    if (bytes == null || bytes.Length == 0)
                    {
                        return null;
                    }

                    return JSON.JsonBack<TEntity>(Encoding.UTF8.GetString(bytes));
                }
            }
            return null;
        }

        public virtual void Save<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity != null)
            {
                string pid = entity.ID.ToString();
                string _listId = typeof(TEntity).Name;

                if (IsExist<TEntity>(entity.ID))
                {
                    _redis.Remove(_listId + pid);
                    _redis.RemoveItemFromList(_listId, pid);

                    _redis.Set(_listId + pid, JSON.Stringify(entity));
                    _redis.AddItemToList(_listId, pid);
                }
                else
                {
                    _redis.Set(_listId + pid, JSON.Stringify(entity));
                    _redis.AddItemToList(_listId, pid);
                }
                _redis.BgSave();
            }
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity != null)
            {
                var _listId = typeof(TEntity).Name;
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
