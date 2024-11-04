using Acme.BookStore.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Mongo
{
    public class MogoRepository : IMongoRepository
    {
        private readonly IMongoDatabase _database;
        public MogoRepository(IMongoDatabase mongoDatabase)
        {
           _database = mongoDatabase;
        }
        public async Task<List<T>> GetAllAsync<T>()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            return await collection.Find(_=>true).ToListAsync();
        }

        public async Task InsertAsync<T>(T entity)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(entity);
        }
    }
}
