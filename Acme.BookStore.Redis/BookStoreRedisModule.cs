using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Acme.BookStore.Repositories;

namespace Acme.BookStore.Redis
{
    [DependsOn(typeof(BookStore.BookStoreDomainModule))]
    public class BookStoreRedisModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var redisConn = configuration.GetSection("Redis");

            var redis = ConnectionMultiplexer.Connect(redisConn["ConnectionString"]);

            Configure<RedisOptions>(configuration.GetSection("Redis")); ///通过IOptions<RedisOptions> options 获取
            ///注入连接客户端
            context.Services.AddSingleton<IConnectionMultiplexer>(redis);
            ///注入仓储
            context.Services.AddTransient<IRedisRepository, RedisRepository>();
           
        }
    }
}
