using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Services
{
    /// <summary>
    /// Represents a local in-memory cache whose values are not serialized.
    /// An abstraction of <see cref="IMemoryCache" /> using a dictionary to store its entries.
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheService" /> class.
        /// </summary>
        /// <param name="cache">The implementation of <see cref="IMemoryCache"/> class.</param>
        public MemoryCacheService(IMemoryCache cache)
        {
            this.Cache = cache;
        }

        /// <summary>
        /// Gets a local in-memory cache whose values are not serialized.
        /// </summary>
        protected IMemoryCache Cache { get; }

        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <returns>The located value or null.</returns>
        public TItem Get<TItem>(string key)
        {
            var item = this.Cache.Get<TItem>(key);

            return item;
        }

        /// <summary>
        /// Gets the item associated with this key if present, if not the getItemCallback is called and item is added to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <param name="getItemCallback">The item creation callback.</param>
        /// <param name="absoluteExpirationMinutes">The absolute expiration time in minutes.</param>
        /// <returns>The located value or the newer created one.</returns>
        public TItem GetOrCreate<TItem>(string key, Func<TItem> getItemCallback, int absoluteExpirationMinutes)
        {
            var item = this.Cache.GetOrCreate(
            key, 
            entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinutes);

                return getItemCallback();
            });

            return item;
        }

        /// <summary>
        /// Gets the item associated with this key if present, if not the getItemCallback is called and item is added to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <param name="getItemCallback">The item creation callback.</param>
        /// <param name="absoluteExpirationMinutes">The absolute expiration time in minutes.</param>
        /// <returns>The located value or the newer created one.</returns>
        public Task<TItem> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> getItemCallback, int absoluteExpirationMinutes)
        {
            var item = this.Cache.GetOrCreateAsync(
            key, 
            entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinutes);

                return getItemCallback();
            });

            return item;
        }

        /// <summary>
        /// Adds entry to the cache by given key and absolute expiration time.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">The key identifying the entry.</param>
        /// <param name="value">The entry's value.</param>
        /// <param name="absoluteExpiration">The absolute expiration time.</param>
        /// <returns>The created entry's value.</returns>
        public TItem Add<TItem>(string key, TItem value, DateTime absoluteExpiration)
        {
            var item = this.Cache.Set(key, value, absoluteExpiration);

            return item;
        }

        /// <summary>
        /// Adds entry to the cache by given key and absolute expiration time to now.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">The key identifying the entry.</param>
        /// <param name="value">The entry's value.</param>
        /// <param name="absoluteExpirationRelativeToNow">The absolute relative expiration time to now.</param>
        /// <returns>The created entry's value.</returns>
        public TItem Add<TItem>(string key, TItem value, TimeSpan absoluteExpirationRelativeToNow)
        {
            var item = this.Cache.Set(key, value, absoluteExpirationRelativeToNow);

            return item;
        }

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">A key identifying the entry.</param>
        public void Remove(string key)
        {
            this.Cache.Remove(key);
        }
    }
}