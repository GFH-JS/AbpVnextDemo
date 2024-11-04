using Acme.BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Data
{
    internal class ProductDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Product, Guid> _repository;
        public ProductDataSeedContributor(IRepository<Product,Guid> repository)
        {
            _repository = repository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _repository.CountAsync() > 0) 
            { 
                return;
            }

            var product = new Product { };

            await _repository.InsertAsync(product);
        }
    }
}
