using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Redis
{
    public class RedisManager
    {
        static object _serObject = new object();
        static object _cliObject = new object();

        static RedisClient _masterRedis = null;

        static Random _clientRedisRan = new Random();
        static RedisClient[] _clientRedis = null;

        static RedisManager()
        {
            var length = RedisSingleton.Current.RedisService.Selves.Count;
            _clientRedis = new RedisClient[length];
        }

        public static RedisClient GetMaster()
        {
            if (_masterRedis == null)
            {
                lock (_serObject)
                {
                    if (_masterRedis == null)
                    {
                        var master = RedisSingleton.Current.RedisService.Master;
                        _masterRedis = new RedisClient(master.Ip, master.Port);
                    }
                }
            }
            return _masterRedis;
        }

        public static RedisClient GetClient()
        {
            var ranInt = _clientRedisRan.Next(_clientRedis.Length);
            if (_clientRedis[ranInt] == null)
            {
                lock (_cliObject)
                {
                    if (_clientRedis[ranInt] == null)
                    {
                        var selve = RedisSingleton.Current.RedisService.Selves[ranInt];
                        _clientRedis[ranInt] = new RedisClient(selve.Ip, selve.Port);
                    }
                }
            }
            return _clientRedis[ranInt];
        }
    }
}
