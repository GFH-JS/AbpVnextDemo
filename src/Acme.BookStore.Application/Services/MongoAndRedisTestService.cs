using Acme.BookStore.Dtos.MongoTest;
using Acme.BookStore.Entities;
using Acme.BookStore.Managers;
using Acme.BookStore.Repositories;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.BookStore.Services
{
    public class MongoAndRedisTestService:BookStoreAppService
    {
        private readonly IMongoRepository _mongorepository;
      private readonly IRedisRepository _redisrepository;
        private readonly IAliyunManger _AliyunManger;
        public MongoAndRedisTestService(IMongoRepository mongoRepository,IRedisRepository redisRepository,IAliyunManger aliyunManger)
        {
            _mongorepository = mongoRepository;
            _redisrepository = redisRepository;
            _AliyunManger = aliyunManger;
        }


        public Task InsertAsync(CreateOrUpdateMongoTestDto mongoTestEntity)
        {
            var vv = ObjectMapper.Map<CreateOrUpdateMongoTestDto, MongoTestEntity>(mongoTestEntity);
            return _mongorepository.InsertAsync(vv);
        }


        public async Task<List<GetMongoTestDto>> GetAllAsync()
        {
            var re = await _mongorepository.GetAllAsync<MongoTestEntity>();
            return ObjectMapper.Map<List<MongoTestEntity>,List<GetMongoTestDto>>(re);
        }

        //Redis测试
        
        public async Task PostStringSetAsync(string key,string value)
        {
            await _redisrepository.SetStringAsync(key, value);
            await _AliyunManger.SendSmsAsync("15061963256","hello");
        }
        public async Task<string> GetStringSetAsync(string key)
        {
            return await _redisrepository.GetStringAsync(key);
        }

        public async Task PostAddSetAsync(string key, string value)
        {
            var re = await _redisrepository.AddToSetAsync(key, value);
            if (!re)
            {
                throw new UserFriendlyException("已存在");
            }

        }
        public async Task<string[]> GetSetAsync(string key)
        {
            
            return await _redisrepository.GetSetAsync(key);
        }


        public async Task<long> PostToListAsync(string key, string value)
        {
            var re = await _redisrepository.PushToListAsync(key, value);

            return re;
        }
        public async Task<string> GetFromListAsync(string key)
        {

            return await _redisrepository.PopFromListAsync(key);
        }

        public async Task<object> GetFromListAllAsync(string key)
        {
            var RE = await _redisrepository.GetListAsync(key);
            return RE;
        }

        public async Task PostHashSetAsync(string key, GetMongoTestDto entity)
        {
            await _redisrepository.SetHashAsync(key,entity);
        }

        public async Task<GetMongoTestDto> GetHashSetAsync(string key)
        {
           return await _redisrepository.GetHashAsync<GetMongoTestDto>(key);
        }

        public async Task PostSortedSetAsync(string key, string value,double score)
        {
            var re = await _redisrepository.AddToSortedSetAsync(key, value,score);
            if (!re)
            {
                throw new UserFriendlyException("已存在");
            }
        }

        public async Task<List<string>> GetSortedSetAsync(string key)
        {
            var re = await _redisrepository.GetSortedSetAsync(key);
            return re;
        }

    }
}
