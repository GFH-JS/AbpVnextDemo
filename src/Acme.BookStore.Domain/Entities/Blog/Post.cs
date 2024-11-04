using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Entities.Blog
{
    public class Post:FullAuditedAggregateRoot<Guid>
    {
        public Blog Blog { get; set; }
        public Guid BlogId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
