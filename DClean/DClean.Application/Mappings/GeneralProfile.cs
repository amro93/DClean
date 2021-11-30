using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VTower.Application.Features.Products.Commands.CreateProduct;
using VTower.Application.Features.Products.Queries.GetAllProducts;
using VTower.Domain.Entities;

namespace VTower.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
