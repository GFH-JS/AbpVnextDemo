using Acme.BookStore.Entities.Category;
using Acme.BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
