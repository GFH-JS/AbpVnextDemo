using Acme.BookStore.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Repositories
{
    public interface IBlogRepository:IRepository<Blog,Guid>
    {
    }
}
