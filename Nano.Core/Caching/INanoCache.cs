using System;
using System.Threading.Tasks;

namespace Nano.Core.Caching
{
    public class CachedObject
    {
        public WeakReference Data { get; set; }
        public TimeSpan? Timeout { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public interface INanoCache
    {
        T Get<T>(string identifier, Func<T> factory, TimeSpan? timeout) where T: class;
        T Get<T>(string identifier) where T : class;
        void Set(string identifier, object data, TimeSpan? timeout);
    }

    public interface INanoCacheAsync: INanoCache
    {
        Task<T> GetAsync<T>(string identifier, Func<Task<T>> factory, TimeSpan? timeout) where T : class;
        Task<T> GetAsync<T>(string identifier) where T : class;
        Task SetAsync(string identifier, object data, TimeSpan? timeout);
    }
}
