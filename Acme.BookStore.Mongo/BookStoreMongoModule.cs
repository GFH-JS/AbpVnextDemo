using Acme.BookStore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Acme.BookStore.Mongo
{
    [DependsOn(typeof(BookStore.BookStoreDomainSharedModule),typeof(BookStore.BookStoreDomainModule))]
    public class BookStoreMongoModule:AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var mongoOption = configuration.GetSection("Mongo").Get<MongoDbOptions>();

            //Configure<MongoDbOptions>(configuration.GetSection("Mongo"));

            context.Services.AddSingleton(mongoOption);

            ///注入client
            context.Services.AddSingleton<IMongoClient>(sp => { 
            
                return new MongoClient(mongoOption.ConnectionString);
            });

            ///注入database
            context.Services.AddSingleton(sp => {

                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoOption.Database);
            } );

            ///注入仓储
            context.Services.AddTransient<IMongoRepository,MogoRepository>();

        }
    }
}
