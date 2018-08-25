using System;
using System.Threading.Tasks;

namespace TicTacToe.Services.Interfaces
{
    public interface ICacheService
    {
        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <returns>The located value or null.</returns>
        TItem Get<TItem>(string key);

        /// <summary>
        /// Gets the item associated with this key if present, if not the getItemCallback is called and item is added to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <param name="getItemCallback">The item creation callback.</param>
        /// <param name="absoluteExpirationMinutes">The absolute expiration time in minutes.</param>
        /// <returns>The located value or the newer created one.</returns>
        TItem GetOrCreate<TItem>(string key, Func<TItem> getItemCallback, int absoluteExpirationMinutes);

        /// <summary>
        /// Gets the item associated with this key if present, if not the getItemCallback is called and item is added to the cache.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">A key identifying the requested entry.</param>
        /// <param name="getItemCallback">The item creation callback.</param>
        /// <param name="absoluteExpirationMinutes">The absolute expiration time in minutes.</param>
        /// <returns>The located value or the newer created one.</returns>
        Task<TItem> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> getItemCallback, int absoluteExpirationMinutes);

        /// <summary>
        /// Adds entry to the cache by given key and absolute expiration time.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">The key identifying the entry.</param>
        /// <param name="value">The entry's value.</param>
        /// <param name="absoluteExpiration">The absolute expiration time.</param>
        /// <returns>The created entry's value.</returns>
        TItem Add<TItem>(string key, TItem value, DateTime absoluteExpiration);

        /// <summary>
        /// Adds entry to the cache by given key and absolute expiration time to now.
        /// </summary>
        /// <typeparam name="TItem">The type of the item.</typeparam>
        /// <param name="key">The key identifying the entry.</param>
        /// <param name="value">The entry's value.</param>
        /// <param name="absoluteExpirationRelativeToNow">The absolute relative expiration time to now.</param>
        /// <returns>The created entry's value.</returns>
        TItem Add<TItem>(string key, TItem value, TimeSpan absoluteExpirationRelativeToNow);

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">A key identifying the entry.</param>
        void Remove(string key);
    }
}