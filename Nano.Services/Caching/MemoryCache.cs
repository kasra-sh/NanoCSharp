using System;
using System.Collections.Concurrent;

namespace Nano.Core.Caching
{
    public class MemoryCache : INanoCache
    {
        private ConcurrentDictionary<string, CachedObject> dic = new ConcurrentDictionary<string, CachedObject>();
        private DateTime minCreatedOn = DateTime.MinValue;
        private TimeSpan minExpireTimespan = TimeSpan.MinValue;

        public MemoryCache()
        {

        }

        public T Get<T>(string identifier, Func<T> factory, TimeSpan? timeout = null) where T : class
        { 
            var now = DateTime.Now;
            if (timeout != null)
                if (timeout < TimeSpan.Zero) return null;

            var hv = dic.TryGetValue(identifier, out var o);
            if (hv)
            {
                return (T) o.Data.Target;
            }

            var data = factory.Invoke();
            Set(identifier, data, timeout);
            return data;
        }

        public T Get<T>(string identifier) where T : class
        {
            var hv = dic.TryGetValue(identifier, out var o);
            if (hv)
            {
                return (T) o.Data.Target;
            }
            else
            {
                return null;
            }
        }

        public void Set(string identifier, object data, TimeSpan? timeout = null)
        {
            var now = DateTime.Now;

            ResetMin(now, timeout??TimeSpan.Zero);
            DeleteExpired(now);

            if (dic.ContainsKey(identifier))
            {
                dic.TryRemove(identifier, out var a);
            }

            dic[identifier] = new CachedObject
            {
                Data = new WeakReference(data),
                CreatedOn = now,
                Timeout = timeout
            };
        }
        
        #region Private

        private void ResetMin(DateTime now, TimeSpan? timeout)
        {
            if (timeout == null) return;

            if (now + timeout < minCreatedOn + minExpireTimespan)
            {
                minCreatedOn = now;
                minExpireTimespan = timeout.Value;
            }
        }

        private void DeleteExpired(DateTime now)
        {
            if (minCreatedOn+minExpireTimespan < DateTime.Now) return;

            foreach (var pair in dic)
            {
                if (now - pair.Value.CreatedOn > pair.Value.Timeout)
                {
                    dic.TryRemove(pair.Key, out var a);
                }
            }
        }

        #endregion

    }

}
