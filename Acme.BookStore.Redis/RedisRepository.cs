using Acme.BookStore.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Redis
{
    public class RedisRepository:IRedisRepository
    {
        private const string RedisKeyPrefix = "BookStore:";
        private readonly IDatabase _database;
        public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }


        #region string
        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _database.StringSetAsync(key,value,expiry);
        }
        public async Task<bool> DeleteStringAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
        public async Task<string> GetStringAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }
        #endregion

        #region list
        public async Task<RedisValue[]> GetListAsync(string key)
        {
            return await _database.ListRangeAsync(key);
        }
        public async Task<string> PopFromListAsync(string key)
        {
            return await _database.ListRightPopAsync(key);
        }

        public async Task<long> PushToListAsync(string key, string value)
        {
            return await _database.ListLeftPushAsync(key, value);
        }
        public async Task<bool> DeleteListAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
        #endregion

        #region hash
        public async Task SetHashAsync<T>(string key,T entity)
        {
            List<HashEntry> hashEntries = new List<HashEntry>();    
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var field = prop.Name;
                var value = prop.GetValue(entity)?.ToString();
                hashEntries.Add(new HashEntry(field,value));
            }
            await _database.HashSetAsync(key, hashEntries.ToArray());
        }
        public async Task SetHashAsync(string key, HashEntry[] hashEntries)
        {
            await _database.HashSetAsync(key, hashEntries);
        }
        public async Task<bool> SetHashFieldAsync(string key, string field, string value)
        {
            return await _database.HashSetAsync(key,field,value);
        }

        public async Task<string> GetHashFieldAsync(string key, string field)
        {
            return await _database.HashGetAsync(key,field);
        }
        public async Task<HashEntry[]> GetHashAsync(string key)
        {
            return await _database.HashGetAllAsync(key);
        }
        public async Task<T> GetHashAsync<T>(string key) where T : new()
        {
            T entity = new T();
            var entries = await _database.HashGetAllAsync(key);
            var prpos = entity.GetType().GetProperties();
            foreach (var prop in prpos) 
            {
                prop.SetValue(entity, Convert.ChangeType(entries.Where(e => e.Name == prop.Name).FirstOrDefault().Value,prop.PropertyType));
            }

            return entity;
        }

        public async Task<bool> DeleteHashAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
        public async Task<bool> DeleteHashFieldAsync(string key, string field)
        {
            return await _database.HashDeleteAsync(key,field);
        }

        #endregion

        #region set
        public async Task<bool> AddToSetAsync(string key, string value)
        {
            return await _database.SetAddAsync(key, value);
        }

        public async Task<bool> DeleteSetAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
        public async Task<string[]> GetSetAsync(string key)
        {
            var values = await _database.SetMembersAsync(key);
            return values.ToStringArray();
        }
        public async Task<bool> RemoveFromSetAsync(string key, string value)
        {
            return await _database.SetRemoveAsync(key, value);
        }
        #endregion

        #region sorted set
        public async Task<bool> AddToSortedSetAsync(string key, string value, double score)
        {
            return await _database.SortedSetAddAsync(key, value, score);
        }
        public async Task<bool> DeleteSortedSetAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
        public async Task<List<string>> GetSortedSetAsync(string key)
        {
            List<string> results = new List<string>();
            var values = await _database.SortedSetRangeByRankWithScoresAsync(key);
            
            foreach (var item in values)
            {
                results.Add(item.Element);
            }
            return results;
        }
        public async Task<bool> RemoveFromSortedSetAsync(string key, string value)
        {
            return await _database.SortedSetRemoveAsync(key, value);
        } 
        #endregion



    }
}
