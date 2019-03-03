using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nano.Core.Caching
{
    public class RedisCache: INanoCacheAsync
    {
        private RedisClient.RedisClient _redisClient;

        public RedisCache(params string[] servers)
        {
            _redisClient = new RedisClient.RedisClient(servers);
        }

        public T Get<T>(string identifier, Func<T> factory, TimeSpan? timeout = null) where T : class
        {
            return GetAsync<T>(identifier, (async () => factory.Invoke())).Result;
        }

        public T Get<T>(string identifier) where T : class
        {
            return GetAsync<T>(identifier).Result;
        }

        public void Set(string identifier, object data, TimeSpan? timeout = null)
        {
            SetAsync(identifier, data, timeout).Wait();
        }

        public async Task<T> GetAsync<T>(string identifier, Func<Task<T>> factory, TimeSpan? timeout = null) where T : class
        {
            if (await _redisClient.KeyExists(identifier))
            {
                return await _redisClient.GetAsync<T>(identifier);
            }
            else
            {
                var d = await factory.Invoke();
                await SetAsync(identifier, d, timeout);
                return d;
            }
        }

        public async Task<T> GetAsync<T>(string identifier) where T : class
        {
            return await _redisClient.GetAsync<T>(identifier);
        }

        public async Task SetAsync(string identifier, object data, TimeSpan? timeout)
        {
            if (timeout.HasValue)
                await _redisClient.SetAsync(identifier, data);
            else
            {
                await _redisClient.SetAsync(identifier, data);
            }
        }
    }
}
