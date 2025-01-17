﻿using Acme.BookStore.Entities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Repositories
{
    public interface ICategoryRepository:IRepository<Category,Guid>
    {
    }
}
