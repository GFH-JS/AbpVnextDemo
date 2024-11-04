using Acme.BookStore.Dtos.Product;
using Acme.BookStore.Entities.Category;
using Acme.BookStore.Entities.Product;
using Acme.BookStore.IServices;
using Acme.BookStore.Repositories;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Acme.BookStore.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.EventBus;
using Acme.BookStore.Etos;
using Volo.Abp.EventBus.Local;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Localization;

namespace Acme.BookStore.Services
{
    public class ProductService : BookStoreAppService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILocalEventBus _eventBus;
        private readonly IDistributedEventBus _distributedEventBus;
        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository,ILocalEventBus eventBus,IDistributedEventBus distributedEventBus)  //IOptions<MongoOptions> options 
        {
           
           _productRepository = productRepository;
           _categoryRepository = categoryRepository;
           _eventBus = eventBus;
           _distributedEventBus = distributedEventBus;
        }
        //[Authorize(BookStorePermissions.Products.create)]
        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            var product = ObjectMapper.Map<CreateUpdateProductDto, Product>(input);   //3a15a17b-28e4-62a6-4a4e-54717a6e0b98
           
            await _productRepository.InsertAsync(product);  //,autoSave:true 单表配置立即保存

            //product.CreateEvent();///聚合根方式触发

            //await UnitOfWorkManager.Current.SaveChangesAsync(); //保存数据库  如不写 方法结束后自动保存 如果异常就会自动回滚

            //await _eventBus.PublishAsync(new ProductEventArgs() {Id = product.Id,Name = product.Name });   //本地事件发布

            await _distributedEventBus.PublishAsync(new ProductEventArgs() { Id = product.Id, Name = product.Name }); //分布式应用发布
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id,true);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(ProductRequestDto input)
        {
            var wel = L["LongWelcomeMessage"];
            var queryable = await _productRepository.WithDetailsAsync(x=>x.Category);  //关联查询
            
            queryable = queryable.WhereIf(!string.IsNullOrEmpty(input.Name), p => p.Name.Contains(input.Name)).OrderBy(input.Sorting??nameof(Product.Name));

            var products = await AsyncExecuter.ToListAsync(queryable.Skip(input.SkipCount).Take(input.MaxResultCount));

            int count = await AsyncExecuter.CountAsync(queryable);
            
            return new PagedResultDto<ProductDto> (
              count,ObjectMapper.Map<List<Product>,List<ProductDto>>(products)
            );
        }

        public async Task<List<ProductDto>> GetProductByCategoryIdAsync(Guid categoryId, int count)
        {
            var queryable = await _productRepository.GetQueryableAsync();
            queryable = queryable.Where(p=>p.CategoryId == categoryId).OrderBy(nameof(Category.CreationTime) + " DESC").Take(count);

            var products = await AsyncExecuter.ToListAsync(queryable);

            return ObjectMapper.Map<List<Product>,List<ProductDto>>(products);
        }

        public async Task<List<ProductDto>> GetProductByGuidAsync(List<Guid> guids)
        {
            var queryable = await _productRepository.GetQueryableAsync();
            queryable = queryable.Where(p => guids.Contains(p.Id)).OrderBy(nameof(Category.CreationTime) + " DESC");
            var products = await AsyncExecuter.ToListAsync(queryable);

            return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await _productRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }

        public string GetTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
