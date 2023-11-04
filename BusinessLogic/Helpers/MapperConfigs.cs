using AutoMapper;
using BusinessLogic.ApiModels.Products;
using BusinessLogic.Dtos;
using BusinessLogic.Entities;

namespace BusinessLogic.Helpers
{
    public class MapperConfigs : Profile
    {
        public MapperConfigs()
        {
            CreateMap<CreateProductModel, Product>();
            CreateMap<EditProductModel, Product>();

            //CreateMap<Product, ProductDto>().ForMember(x => x.CategoryName, cfg =>
            //{
            //    cfg.MapFrom(x => x.Category.Name);
            //});
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
