using Acme.BookStore.Dtos.Category;
using Acme.BookStore.Entities.Category;
using Acme.BookStore.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IO;

namespace Acme.BookStore.Services
{
    public class CategoryService : CrudAppService<Category, CategoryDto, Guid, PagedAndSortedResultRequestDto,CreateUpdateCategoryDto>, ICategoryService
    {
        public CategoryService(IRepository<Category, Guid> repository) : base(repository)
        {
           
        }


       
    }
}
