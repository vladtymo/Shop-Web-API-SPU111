using AutoMapper;
using BusinessLogic.ApiModels.Products;
using BusinessLogic.Dtos;
using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
