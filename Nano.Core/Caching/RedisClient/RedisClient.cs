using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace Nano.Core.Caching.RedisClient
{
    public class RedisClient: IDisposable
    {
        private static readonly ConnectionMultiplexer ConnectionMultiplexer;
        private readonly IDatabase _db;

//        static RedisClient()
//        {
//            ConnectionMultiplexer = ConnectionMultiplexer.Connect("localhost");
//        }

        public static RedisClient GetDefault
        {
            get
            {
                return new RedisClient("localhost");
            }
        }

        public RedisClient(params string[] servers)
        {
            _db = ConnectionMultiplexer.GetDatabase();
        }

        public IDatabase GetDb()
        {
            return _db;
        }

        public async Task<bool> KeyExists(string key)
        {
            var res = await _db.KeyExistsAsync(key);
            return res;
        }

        public async Task<bool> SetAsync(string key, object obj, TimeSpan expire)
        {
            var res = await SetAsync(key, obj);
            await _db.KeyExpireAsync(key, expire);
            return res;
        }

        public async Task<bool> SetAsync(string key, object obj)
        {
            var res = await _db.StringSetAsync(key, JToken.FromObject(obj).ToString());
            return res;
        }

        public async Task<bool> Delete(string key)
        {
            var res = await _db.KeyDeleteAsync(key);
            return res;
        }

        public async Task<TModel> GetAsync<TModel>(string key) where TModel: class 
        {
            var val = await _db.StringGetAsync(key);

            if (val.HasValue)
            {
                return JToken.Parse(val).ToObject<TModel>();
            }

            return null;
        }

        public void Dispose()
        {
            try
            {
                ConnectionMultiplexer.Close(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}