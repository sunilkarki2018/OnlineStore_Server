using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            CreateMap<Product, ProductReadDTO>().ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name));
            CreateMap<ProductCreateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());
            CreateMap<ProductUpdateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.FirstName, option => option.MapFrom(source => source.User.FirstName))
            .ForMember(dest => dest.LastName, option => option.MapFrom(source => source.User.LastName))
            .ForMember(dest => dest.Email, option => option.MapFrom(source => source.User.Email));
            CreateMap<OrderCreateDTO, Order>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<OrderUpdateDTO, Order>()
            .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Review, ReviewReadDTO>()
             .ForMember(dest => dest.FirstName, option => option.MapFrom(source => source.User.FirstName))
            .ForMember(dest => dest.LastName, option => option.MapFrom(source => source.User.LastName))
            .ForMember(dest => dest.ProductName, option => option.MapFrom(source => source.Product.Title));
            CreateMap<ReviewCreateDTO, Review>()
             .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}