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
        static object _object = new object();
        static RedisClient _redis = null;

        public static RedisClient GetClient()
        {
            if (_redis == null)
            {
                lock (_object)
                {
                    if (_redis == null)
                    {
                        _redis = new RedisClient("127.0.0.1", 6379);
                    }
                }
            }
            return _redis;
        }
    }
}
