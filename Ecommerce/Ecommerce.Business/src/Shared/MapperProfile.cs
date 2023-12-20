using AutoMapper;
using Ecommerce.Business.src.DTOs;
using Ecommerce.Core.src.Entities;

namespace Ecommerce.Business.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDTO>().ForMember(dest => dest.AddressReadDTO, opt => opt.MapFrom(src => src.Address));
            CreateMap<UserCreateDTO, User>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AddressCreateDTO));
            CreateMap<UserUpdateDTO, User>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AddressUpdateDTO));

            CreateMap<Address, AddressReadDTO>();
            CreateMap<AddressCreateDTO, Address>();
            CreateMap<AddressUpdateDTO, Address>();

            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<Product, ProductReadDTO>();
            CreateMap<ProductCreateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());
            CreateMap<ProductUpdateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<AvatarCreateDTO, Avatar>();
            CreateMap<Avatar, AvatarReadDTO>();

            CreateMap<Order, OrderReadDTO>().ForMember(dest => dest.orderItemReadDTOs, opt => opt.MapFrom(src => src.orderItems));
            CreateMap<OrderCreateDTO, Order>().ForMember(dest => dest.orderItems, opt => opt.MapFrom(src => src.orderItemCreateDTOs));
            CreateMap<OrderUpdateDTO, Order>();

            CreateMap<OrderItem, OrderItemReadDTO>();
            CreateMap<OrderItemCreateDTO, OrderItem>();


            /*   CreateMap<Product, ProductReadDTO>().ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.Category.Name));
              CreateMap<ProductCreateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());
              CreateMap<ProductUpdateDTO, Product>().ForMember(dest => dest.Category, opt => opt.Ignore());

              CreateMap<Category, CategoryReadDTO>();
              CreateMap<CategoryCreateDTO, Category>();
              CreateMap<CategoryUpdateDTO, Category>();

              CreateMap<Order, OrderReadDTO>()
              .ForMember(dest => dest.FirstName, option => option.MapFrom(source => source.User.FirstName))
              .ForMember(dest => dest.LastName, option => option.MapFrom(source => source.User.LastName))
              .ForMember(dest => dest.Email, option => option.MapFrom(source => source.User.Email));

              CreateMap<OrderItem, OrderItemReadDTO>();

              CreateMap<OrderCreateDTO, Order>()
              .ForMember(dest => dest.User, opt => opt.Ignore());
              CreateMap<OrderUpdateDTO, Order>()
              .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
              .ForMember(dest => dest.User, opt => opt.Ignore());

              CreateMap<Review, ReviewReadDTO>()
               .ForMember(dest => dest.FirstName, option => option.MapFrom(source => source.User.FirstName))
              .ForMember(dest => dest.LastName, option => option.MapFrom(source => source.User.LastName))
              .ForMember(dest => dest.ProductName, option => option.MapFrom(source => source.Product.Title));
              CreateMap<ReviewCreateDTO, Review>()
               .ForMember(dest => dest.Product, opt => opt.Ignore())
              .ForMember(dest => dest.User, opt => opt.Ignore()); */
        }
    }
}