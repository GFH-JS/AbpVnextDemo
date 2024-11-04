using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Repositories
{
    public interface IRedisRepository
    {
        // String
        Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null);
        Task<string> GetStringAsync(string key);
        Task<bool> DeleteStringAsync(string key);

        // Hash
        Task<bool> SetHashFieldAsync(string key, string field, string value);
        Task SetHashAsync<T>(string key, T entity);
        Task SetHashAsync(string key, HashEntry[] hashEntries);
        Task<string> GetHashFieldAsync(string key, string field);
        Task<HashEntry[]> GetHashAsync(string key);
        Task<T> GetHashAsync<T>(string key) where T: new();
        Task<bool> DeleteHashFieldAsync(string key, string field);
        Task<bool> DeleteHashAsync(string key);

        // List
        Task<long> PushToListAsync(string key, string value);
        Task<RedisValue[]> GetListAsync(string key);
        Task<string> PopFromListAsync(string key);
        Task<bool> DeleteListAsync(string key);

        // Set
        Task<bool> AddToSetAsync(string key, string value);
        Task<string[]> GetSetAsync(string key);
        Task<bool> RemoveFromSetAsync(string key, string value);
        Task<bool> DeleteSetAsync(string key);

        // Sorted Set
        Task<bool> AddToSortedSetAsync(string key, string value, double score);
        Task<List<string>> GetSortedSetAsync(string key);
        Task<bool> RemoveFromSortedSetAsync(string key, string value);
        Task<bool> DeleteSortedSetAsync(string key);
    }
}
