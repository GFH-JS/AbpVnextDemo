using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Repositories
{
    public interface IMongoRepository
    {
        Task InsertAsync<T>(T entity);

        Task<List<T>> GetAllAsync<T>();
    }
}
