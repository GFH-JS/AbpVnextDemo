using Acme.BookStore.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.IServices
{
    public interface IProductService:IApplicationService
    {
        Task<PagedResultDto<ProductDto>> GetListAsync(ProductRequestDto input);

        Task CreateAsync(CreateUpdateProductDto input);

        Task<ProductDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, CreateUpdateProductDto input);

        Task DeleteAsync(Guid id);

        Task<List<ProductDto>> GetProductByCategoryIdAsync(Guid categoryId, int count);

        Task<List<ProductDto>> GetProductByGuidAsync(List<Guid> guids);
    }
}
