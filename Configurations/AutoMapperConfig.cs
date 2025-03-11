using AutoMapper;
using Core.Entities;
using Application.Dto;
using Application.Dtos.Request;

namespace WebAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductRequest, Product>();
        }
    }
}