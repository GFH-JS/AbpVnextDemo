using Acme.BookStore.Dtos.Category;
using Acme.BookStore.Dtos.MongoTest;
using Acme.BookStore.Dtos.Product;
using Acme.BookStore.Entities;
using Acme.BookStore.Entities.Category;
using Acme.BookStore.Entities.Product;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CreateOrUpdateMongoTestDto, MongoTestEntity>();
        CreateMap<MongoTestEntity, GetMongoTestDto>();

        CreateMap<Product, ProductDto>().ForMember(dest => dest.CategoryName ,opt=>opt.MapFrom(s=>s.Category.Name));
        CreateMap<CreateUpdateProductDto, Product>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();
    }
}
