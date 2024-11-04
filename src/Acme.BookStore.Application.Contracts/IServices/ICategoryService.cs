using Acme.BookStore.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.IServices
{
    public interface ICategoryService:ICrudAppService<CategoryDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateCategoryDto>
    {
    }
}
