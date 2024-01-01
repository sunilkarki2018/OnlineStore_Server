using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<UserCreateDTO, User>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            CreateMap<UserUpdateDTO, User>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<Address, AddressReadDTO>();
            CreateMap<AddressCreateDTO, Address>();
            CreateMap<AddressUpdateDTO, Address>();

            CreateMap<ProductLine, ProductLineReadDTO>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<ProductLineCreateDTO, ProductLine>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images)).ForMember(dest => dest.Category, opt => opt.Ignore());
            CreateMap<ProductLineUpdateDTO, ProductLine>().ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>().ForMember(dest => dest.ProductLine, opt => opt.Ignore())
                .ForMember(dest => dest.ProductSize, opt => opt.Ignore());
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<ProductSize, ProductSizeReadDTO>();
            CreateMap<ProductSizeCreateDTO, ProductSize>();
            CreateMap<ProductSizeUpdateDTO, ProductSize>();

            CreateMap<ImageCreateDTO, Image>();
            CreateMap<Image, ImageReadDTO>();
            CreateMap<ImageUpdateDTO, Image>();

            CreateMap<AvatarCreateDTO, Avatar>();
            CreateMap<Avatar, AvatarReadDTO>();

            CreateMap<Order, OrderReadDTO>().ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.orderItems));
            CreateMap<OrderCreateDTO, Order>().ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderUpdateDTO, Order>();

            CreateMap<OrderItem, OrderItemReadDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>();

            CreateMap<Review, ReviewReadDTO>();
            CreateMap<ReviewCreateDTO, Review>();

        }
    }
}