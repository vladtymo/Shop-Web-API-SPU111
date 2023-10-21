﻿using AutoMapper;
using BusinessLogic.ApiModels.Products;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopSPUDbContext ctx;
        private readonly IMapper mapper;

        public ProductsService(ShopSPUDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public void Create(CreateProductModel product)
        {
            //var entity = new Product()
            //{
            //    Name = product.Name,
            //    CategoryId = product.CategoryId,
            //    Description = product.Description,
            //    Discount = product.Discount,
            //    ImageUrl = product.ImageUrl,
            //    InStock = product.InStock,
            //    Price = product.Price
            //};

            ctx.Products.Add(mapper.Map<Product>(product));
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) return; // TODO: generate exception

            ctx.Products.Remove(item);
            ctx.SaveChanges();
        }

        public void Edit(EditProductModel product)
        {
            //var entity = new Product()
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    CategoryId = product.CategoryId,
            //    Description = product.Description,
            //    Discount = product.Discount,
            //    ImageUrl = product.ImageUrl,
            //    InStock = product.InStock,
            //    Price = product.Price
            //};

            ctx.Products.Update(mapper.Map<Product>(product));
            ctx.SaveChanges();
        }

        public List<Product> Get()
        {
            return ctx.Products.Include(x => x.Category).ToList();
        }

        public Product? Get(int id)
        {
            var item = ctx.Products.Find(id);

            if (item == null) return null;

            return item;
        }
    }
}
